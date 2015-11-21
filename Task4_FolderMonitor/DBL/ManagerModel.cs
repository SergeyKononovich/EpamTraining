using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Task4_FolderMonitor.DBL
{
    public class ManagerModel : ModelBase
    {
        [Required]
        [MaxLength(30)]
        public string SecondName { get; set; }

        public virtual ICollection<SaleModel> Dealings { get; set; }
    }
}
