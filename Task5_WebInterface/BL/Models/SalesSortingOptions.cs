using System.Collections.Generic;

namespace BL.Models
{
    public enum SaleOrderColumn
    {
        Id,
        Date,
        Goods,
        Manager,
        Client,
        SellingPrice
    }

    public class SaleSortOption
    {
        public SaleOrderColumn Column { get; set; }
        public bool Ascending { get; set; } = true;
    }

    public class SalesSortingOptions
    {
        public IList<SaleSortOption> Options { get; set; } = new List<SaleSortOption>();
    }
}
