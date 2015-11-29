using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity.Infrastructure;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;
using Common.Logging;
using FileHelpers;
using Task4_FolderMonitor.BL.Entities;
using Task4_FolderMonitor.BL.IDAL;
using Task4_FolderMonitor.BL.Utils;
using Task4_FolderMonitor.DAL;
using Task4_FolderMonitor.FolderMonitor.IBL;

namespace Task4_FolderMonitor.BL
{
    public class FolderMonitor : IFolderMonitor
    {
        private static readonly ILog Log = LogManager.GetLogger("FolderMonitor");

        private readonly object _locker = new object();
        private readonly IDAOFactory _daoFactory;
        private readonly FileSystemWatcher _watcher;
        private readonly Regex _directoryNameFormat;
        private readonly CancellationTokenSource _cancellationTokenSource;
        private readonly LimitedConcurrencyLevelTaskScheduler _scheduler;
        private readonly List<Task> _runningTasks = new List<Task>(); 
        private readonly string _directoryForProcessedFiles;

        public bool IsStarted { get; private set; }
        

        public FolderMonitor()
            : this(new DAOFactory())
        {
        }
        public FolderMonitor(IDAOFactory daoFactory)
        {
            if (daoFactory == null) throw new ArgumentNullException(nameof(daoFactory));

            string directory;
            int maxDegreeOfParallelism;
            LoadSettingsFromConfig(out directory, out _directoryForProcessedFiles, out maxDegreeOfParallelism);

            _daoFactory = daoFactory;
            _watcher = new FileSystemWatcher(directory) {Filter = "*.csv"};
            _directoryNameFormat = new Regex(@"^[A-Z][a-z]{0,29}_\d{8}$");
            _cancellationTokenSource = new CancellationTokenSource();
            _scheduler = new LimitedConcurrencyLevelTaskScheduler(maxDegreeOfParallelism);
        }


        public void Start()
        {
            lock (_locker)
            {
                if (IsStarted)
                    return;

                IsStarted = true;
                _watcher.Created += WatcherOnCreated;
                _watcher.EnableRaisingEvents = true;
            }
        }
        public void Stop()
        {
            lock (_locker)
            {
                if (!IsStarted)
                    return;

                _watcher.Created -= WatcherOnCreated;
                _watcher.EnableRaisingEvents = false;
                IsStarted = false;
                
                Task.WaitAll(_runningTasks.ToArray());
                _runningTasks.Clear();
                _cancellationTokenSource.Cancel(throwOnFirstException: false);
            }
        }
        public void Cancel()
        {
            lock (_locker)
            {
                _cancellationTokenSource.Cancel(throwOnFirstException: false);
                Stop();
            }
        }

        private void LoadSettingsFromConfig(out string directory, out string directoryForProcessedFiles, 
                                            out int maxDegreeOfParallelism)
        {
            directory = ConfigurationManager.AppSettings["directory"];
            if (directory == null || !Directory.Exists(directory))
                throw new DirectoryNotFoundException("Сonfig file contains a non-existent " +
                                                     $"directory: {directory}");

            directoryForProcessedFiles = ConfigurationManager.AppSettings["directoryForProcessedFiles"];
            if (_directoryForProcessedFiles == null || !Directory.Exists(_directoryForProcessedFiles))
                throw new DirectoryNotFoundException("Сonfig file contains a non-existent " +
                                                     $"directory: {_directoryForProcessedFiles}");

            maxDegreeOfParallelism = int.Parse(ConfigurationManager.AppSettings["maxDegreeOfParallelism"]);
            if (maxDegreeOfParallelism < 1)
                throw new ArgumentOutOfRangeException("maxDegreeOfParallelism",
                                                      "Сonfig file contains a non-valid value: " +
                                                      "maxDegreeOfParallelism must be greater than 0");

            Log.Trace("\nConfig:\n" +
                      $"   directory                  [{directory}]\n" +
                      $"   directoryForProcessedFiles [{directoryForProcessedFiles}]\n" +
                      $"   maxDegreeOfParallelism     [{maxDegreeOfParallelism}]");
        }
        private void WatcherOnCreated(object sender, FileSystemEventArgs fileSystemEventArgs)
        {
            if (!File.Exists(fileSystemEventArgs.FullPath))
                return;

            Task copy = null; // initialized after the creation of the task
            var t = new Task(() =>
            {
                lock (_locker)
                {
                    if (_cancellationTokenSource.IsCancellationRequested)
                        return;

                    _runningTasks.Add(copy);
                }

                try
                {
                    Utilities.TrySeveralTimesIfException(numTries: 3, timeSleepInMs: 50, action: (int tries) =>
                    {
                        IDAO dao;
                        CancellationToken token;
                        lock (_locker)
                        {
                            dao = _daoFactory.CreateDAO();
                            token = _cancellationTokenSource.Token;
                        }

                        ProcessTask(dao, fileSystemEventArgs.FullPath,
                            fileSystemEventArgs.Name, token);

                    }, ignoredExceptions: new List<Type> { typeof(DbUpdateException) });
                }
                catch (Exception e)
                {
                    Log.Error($"Thread [{Thread.CurrentThread.ManagedThreadId}] - " +
                              $"File [{fileSystemEventArgs.Name}] not processed", e);
                }
            });

            copy = t;
            t.ContinueWith(task => _runningTasks.Remove(t));
            t.Start(_scheduler);
        }
        private void ProcessTask(IDAO dao, string fullPath, string fileName, CancellationToken token)
        {
            token.ThrowIfCancellationRequested();

            using (dao)
            using (var scope = new TransactionScope(TransactionScopeOption.Suppress, 
                                                    new TransactionOptions
                                                        { IsolationLevel = IsolationLevel.ReadCommitted }))
            {
                try
                {
                    ProcessFile(dao, fullPath, fileName, token);

                    File.Move(fullPath, Path.Combine(_directoryForProcessedFiles, fileName));
                    scope.Complete();
                }
                catch (Exception e)
                when (e is TaskCanceledException == false)
                {
                    Log.Error($"Thread [{Thread.CurrentThread.ManagedThreadId}] - " +
                              $"An error occurred while processing file [{fileName}]", e.GetBaseException());
                    throw;
                }
            }
        }
        private void ProcessFile(IDAO dao, string fullPath, string fileName, CancellationToken token)
        {
            Log.Trace($"Thread [{Thread.CurrentThread.ManagedThreadId}] - Start processing file [{fileName}]");

            token.ThrowIfCancellationRequested();
            string managerSecondName = GetManagerSecondNameFromFileName(fileName);
            var manager = dao.ManagerRepository.FindBySecondName(managerSecondName) ??
                          dao.ManagerRepository.Add(new Manager(managerSecondName));
            
            token.ThrowIfCancellationRequested();
            // parsing .csv
            using (var textReader = File.OpenText(fullPath))
            {
                var engine = new FileHelperEngine<SaleRecord>();
                var records = engine.ReadStream(textReader);

                foreach (var record in records)
                {
                    token.ThrowIfCancellationRequested();
                    ProcessRecord(dao, record, manager, fileName, token);
                }
            }
        }
        private static void ProcessRecord(IDAO dao, SaleRecord record, Manager manager, 
                                          string fileName, CancellationToken token)
        {
            token.ThrowIfCancellationRequested();
            var client = dao.ClientRepository.FindByFullName(record.ClientFullName) ??
                         dao.ClientRepository.Add(new Client(record.ClientFullName));

            token.ThrowIfCancellationRequested();
            var goods = dao.GoodsRepository.FindByName(record.GoodsName) ??
                        dao.GoodsRepository.Add(new Goods(record.GoodsName, record.Cost));

            var sale = new Sale(manager, client, goods)
            {
                SellingPrice = record.Cost,
                Date = record.Date
            };

            token.ThrowIfCancellationRequested();
            sale = dao.SaleRepository.Add(sale);
            Log.Trace($"Thread [{Thread.CurrentThread.ManagedThreadId}] - " +
                      $"FileName [{fileName}] - " +
                      $"Record added [Manager: {manager.SecondName}, Client: {client.FullName}, " +
                      $"Goods: {goods.Name}, Cost: {sale.SellingPrice}, Date: {sale.Date.Year}]");
        }
        private string GetManagerSecondNameFromFileName(string fileName)
        {
            fileName = Path.GetFileNameWithoutExtension(fileName);
            lock (_locker)
                if (fileName == null || !_directoryNameFormat.IsMatch(fileName))
                    throw new FormatException($"File name - '{fileName}' must be in the format '{_directoryNameFormat}'");
            
            return Regex.Split(fileName, "_")[0];
        }
    }
}