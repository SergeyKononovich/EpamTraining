using System;
using System.Threading;
using Common.Logging;
using Task4_FolderMonitor.BL;
using Task4_FolderMonitor.Data;
using Task4_FolderMonitor.Data.Entities;

namespace Task4_FolderMonitor
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                ClearDB();

                LogManager.GetLogger<Program>().Trace(Thread.CurrentThread.ManagedThreadId);
                var monitor = new FolderMonitor();
                monitor.Start();

                Console.WriteLine("Press key to cancel...");
                Console.ReadKey();
                monitor.Cancel();
            }
            catch (Exception e)
            {
                LogManager.GetLogger<Program>().Trace(e);
            }

             Console.ReadKey();
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
