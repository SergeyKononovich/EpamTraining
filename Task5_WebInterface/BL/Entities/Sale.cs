using System;

namespace BL.Entities
{
    public class Sale
    {
        public Sale(Manager manager, Client client, Goods goods)
        {
            if (manager == null) throw new ArgumentNullException(nameof(manager));
            if (client == null) throw new ArgumentNullException(nameof(client));
            if (goods == null) throw new ArgumentNullException(nameof(goods));

            Manager = manager;
            Client = client;
            Goods = goods;
        }


        public int Id { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
        public Manager Manager { get; }
        public Client Client { get; }
        public Goods Goods { get; }
        public int SellingPrice { get; set; }
    }
}
