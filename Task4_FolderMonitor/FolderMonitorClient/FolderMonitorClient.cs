using System;
using Task4_FolderMonitor.BL;
using Task4_FolderMonitor.FolderMonitorClient.IBL;

namespace Task4_FolderMonitor.FolderMonitorClient
{
    public class FolderMonitorClient
    {
        private readonly IFolderMonitor _folderMonitor;


        public FolderMonitorClient()
            : this(new FolderMonitor())
        {
        }
        public FolderMonitorClient(IFolderMonitor folderMonitor)
        {
            if (folderMonitor == null) throw new ArgumentNullException(nameof(folderMonitor));

            _folderMonitor = folderMonitor;
        }
    }
}