using System;

namespace WebInterface.Models
{
    public class ClientModel
    {
        public int Id { get; set; }
        public string FullName { get; set; }
    }

    public class GoodsModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Cost { get; set; }
    }

    public class ManagerModel
    {
        public int Id { get; set; }
        public string SecondName { get; set; }
    }

    public class SaleModel
    {
        public int Id { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
        public ManagerModel Manager { get; set; }
        public ClientModel Client { get; set; }
        public GoodsModel Goods { get; set; }
        public int SellingPrice { get; set; }
    }
}