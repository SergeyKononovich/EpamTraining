using System;
using System.ServiceProcess;
using Task4_FolderMonitor.FolderMonitor.IBL;

namespace Task4_FolderMonitor.FolderMonitor
{
    partial class FolderMonitorService : ServiceBase
    {
        private readonly IFolderMonitor _folderMonitor;


        public FolderMonitorService()
            : this(new BL.FolderMonitor())
        {
        }
        public FolderMonitorService(IFolderMonitor folderMonitor)
        {
            if (folderMonitor == null) throw new ArgumentNullException(nameof(folderMonitor));

            InitializeComponent();
            _folderMonitor = folderMonitor;
        }

        
        protected override void OnStart(string[] args)
        {
            _folderMonitor.Start();

            base.OnStart(args);
        }
        protected override void OnStop()
        {
            _folderMonitor.Stop();

            base.OnStop();
        }
    }
}
