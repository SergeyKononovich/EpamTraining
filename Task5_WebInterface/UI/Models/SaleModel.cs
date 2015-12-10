using System;

namespace BL.Models
{
    public class SaleModel : ModelBase
    {
        public DateTime Date { get; set; }
        public ManagerModel Manager { get; set; }
        public ClientModel Client { get; set; }
        public GoodsModel Goods { get; set; }
        public int SellingPrice { get; set; }
    }
}
