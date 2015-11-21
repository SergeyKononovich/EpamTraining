using System;
using Task4_FolderMonitor.DBL;

namespace Task4_FolderMonitor
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var db = new StoreContext())
            {
                Console.WriteLine(db.CreateDatabaseScript());
            }

            Console.ReadKey();
        }
    }
}
