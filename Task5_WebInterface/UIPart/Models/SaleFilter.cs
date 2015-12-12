using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UIPart.Models
{
    public class SaleFilter
    {
        public int FromId { get; set; }
        public int ToId { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public string GoodsNamePart { get; set; }
        public string ManagerSecondNamePart { get; set; }
        public string ClientFullNamePart { get; set; }
        public int FromPrice { get; set; }
        public int ToPrice { get; set; }
    }
}
