using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Task4_FolderMonitor.DBL
{
    public class ClientModel : ModelBase
    {
        [Required]
        [MaxLength(50)]
        public string FullName { get; set; }

        public virtual ICollection<SaleModel> Dealings { get; set; }
    }
}
