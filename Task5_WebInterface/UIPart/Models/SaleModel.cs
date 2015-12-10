using System;

namespace UIPart.Models
{
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