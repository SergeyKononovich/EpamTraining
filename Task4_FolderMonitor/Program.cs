using System;
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
                using (var db = new StoreContext())
                {
                    Console.WriteLine(db.CreateDatabaseScript());
                }

                var sale = new Sale(new Manager("Ivanov"), new Client("Petr Gudei"), new Goods("Suga", 100000));
                using (var dao = new DAO())
                {
                    dao.SaleRepository.Add(sale);

                    foreach (var s in dao.SaleRepository.List)
                        Console.WriteLine($"{s.Id} {s.Goods.Name} {s.Goods.Cost}");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            Console.ReadKey();
        }
    }
}
