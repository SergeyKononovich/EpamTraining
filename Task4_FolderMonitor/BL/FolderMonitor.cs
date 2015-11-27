using System;
using System.Configuration;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using Common.Logging;
using FileHelpers;
using Task4_FolderMonitor.BL.Entities;
using Task4_FolderMonitor.BL.IDAL;
using Task4_FolderMonitor.BL.Utils;
using Task4_FolderMonitor.DAL;
using Task4_FolderMonitor.FolderMonitorClient.IBL;

namespace Task4_FolderMonitor.BL
{
    public class FolderMonitor : IFolderMonitor
    {
        private static readonly ILog Log = LogManager.GetLogger<FolderMonitor>();

        public object Locker { get; } = new object();
        private IDAO DAO { get; }
        private FileSystemWatcher Watcher { get; }

        
        public FolderMonitor()
            : this(new DAO())
        {
        }
        public FolderMonitor(IDAO dao)
        {
            if (dao == null) throw new ArgumentNullException(nameof(dao));
            string directory = ConfigurationManager.AppSettings["directory"];
            if (directory == null || !Directory.Exists(directory))
                throw new DirectoryNotFoundException("Сonfig file contains a non-existent directory");

            DAO = dao;
            Watcher = new FileSystemWatcher(directory);

            ConfigureWatcher();
        }

        private void ConfigureWatcher()
        {
            Watcher.Filter = "*.csv";
            
            Watcher.Created += WatcherOnCreatedAsync;
            Watcher.EnableRaisingEvents = true;
        }
        private void WatcherOnCreatedAsync(object sender, FileSystemEventArgs fileSystemEventArgs)
        {
            if (!File.Exists(fileSystemEventArgs.FullPath))
                return;

            try
            {
                var newFile = File.OpenText(fileSystemEventArgs.FullPath);
                ProcessFile(newFile, fileSystemEventArgs.Name);
            }
            catch (Exception e) 
            when(e is FileNotFoundException || e is FileLoadException)
            {
                Log.Error($"Record process error: {e.Message}");
            }
        }
        private void ProcessFile(TextReader file, string fileName)
        {
            string managerSecondName = GetManagerSecondNameFromFileName(fileName);
            if (managerSecondName == null) return;

            Manager manager;
            lock (Locker)
                manager = DAO.ManagerRepository.FindBySecondName(managerSecondName) ??
                          new Manager(managerSecondName);

            var engine = new FileHelperEngine<SaleRecord>();
            var records = engine.ReadStream(file);

            try
            {
                foreach (var record in records)
                    ProcessRecord(record, manager);
            }
            catch (Exception e)
            {
                Log.Error($"Record process error: {e.Message}");
            }
        }
        private void ProcessRecord(SaleRecord record, Manager manager)
        {
            lock (Locker)
            {
                var client = DAO.ClientRepository.FindByFullName(record.ClientFullName) ??
                             new Client(record.ClientFullName);

                var goods = DAO.GoodsRepository.FindByName(record.GoodsName) ??
                            new Goods(record.GoodsName, record.Cost);

                var sale = new Sale(manager, client, goods)
                {
                    SellingPrice = record.Cost,
                    Date = record.Date
                };

                DAO.SaleRepository.Add(sale);
                Log.Trace($"Record added [Date: {sale.Date}, Goods: {goods.Name}, " +
                      $"Manager: {manager.SecondName}, Client: {client.FullName}, Cost: {sale.SellingPrice}]");
            }
        }

        private static string GetManagerSecondNameFromFileName(string fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName)) throw new ArgumentNullException(nameof(fileName));
            
            return Regex.Split(fileName, "_")[0];
        }
    }
}