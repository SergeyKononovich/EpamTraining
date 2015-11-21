using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Task4_FolderMonitor.DBL
{
    public class GoodsModel : ModelBase
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        public int Cost { get; set; }

        public virtual ICollection<SaleModel> Dealings { get; set; }
    }
}
