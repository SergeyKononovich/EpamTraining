using System;

namespace Data.Models
{
    public class SaleModel : ModelBase
    {
        public DateTime Date { get; set; }
        public int ManagerId { get; set; }
        public virtual ManagerModel Manager { get; set; }
        public int ClientId { get; set; }
        public virtual ClientModel Client { get; set; }
        public int GoodsId { get; set; }
        public virtual GoodsModel Goods { get; set; }
        public int SellingPrice { get; set; }
    }
}
