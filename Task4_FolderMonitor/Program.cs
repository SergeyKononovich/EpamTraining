using System;
using System.Reflection;
using System.ServiceProcess;
using System.Threading;
using Common.Logging;
using Task4_FolderMonitor.Data;
using Task4_FolderMonitor.FolderMonitor;

namespace Task4_FolderMonitor
{
    class Program
    {
        private static readonly ILog Log = LogManager.GetLogger("Program");

        static void Main(string[] args)
        {
            //ClearDB();
            
            if (!Environment.UserInteractive)
            {
                ServiceBase[] servicesToRun = { new FolderMonitorService() };
                Log.Trace("Service starting");
                ServiceBase.Run(servicesToRun);
            }
            else
            {
                var client = new FolderMonitorClient();
                Log.Trace("Client starting");
                client.Start();
            }
        }

        static void ClearDB()
        {
            using (var db = new StoreContext())
            {
                db.Sales.RemoveRange(db.Sales);
                db.Goods.RemoveRange(db.Goods);
                db.Clients.RemoveRange(db.Clients);
                db.Managers.RemoveRange(db.Managers);
                db.SaveChanges();
            }
        }
    }
}
