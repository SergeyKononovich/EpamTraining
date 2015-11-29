using System;

namespace Task4_FolderMonitor.Data.Entities
{
    public class SaleEntity : IEntity
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int ManagerId { get; set; }
        public virtual ManagerEntity Manager { get; set; }
        public int ClientId { get; set; }
        public virtual ClientEntity Client { get; set; }
        public int GoodsId { get; set; }
        public virtual GoodsEntity Goods { get; set; }
        public int SellingPrice { get; set; }
    }
}
