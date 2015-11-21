using System;
using System.ComponentModel.DataAnnotations;

namespace Task4_FolderMonitor.DBL
{
    public class SaleModel : ModelBase
    {
        public DateTime Date { get; set; }
        public virtual ManagerModel Manager { get; set; }
        public virtual ClientModel Client { get; set; }
        public virtual GoodsModel Goods { get; set; }
        [Range(0, int.MaxValue)]
        public int SellingPrice { get; set; }
    }
}
