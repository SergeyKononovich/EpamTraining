using System.Collections.Generic;

namespace UIPart.Models
{
    public class SalesPage
    {
        public IList<SaleModel> Displayed { get; set; } = new List<SaleModel>();
        public int TotalRecords { get; set; }
        public int TotalDisplayRecords { get; set; }
    }
}