using System;
using System.Threading.Tasks;
using Task4_FolderMonitor.BL;
using Task4_FolderMonitor.BL.Entities;
using Task4_FolderMonitor.Data;
using Task4_FolderMonitor.DAL;

namespace Task4_FolderMonitor
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var monitor = new FolderMonitor();
                Console.WriteLine("Press key to close...");
                Console.ReadKey();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
