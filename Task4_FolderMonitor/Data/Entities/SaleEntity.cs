using System;
using System.ComponentModel.DataAnnotations;

namespace Task4_FolderMonitor.Data.Entities
{
    public class SaleEntity
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public virtual ManagerEntity Manager { get; set; }
        public virtual ClientEntity Client { get; set; }
        public virtual GoodsEntity Goods { get; set; }
        public int SellingPrice { get; set; }
    }
}
