using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Task4_FolderMonitor.Data.Entities
{
    public class ManagerEntity
    {
        public int Id { get; set; }
        public string SecondName { get; set; }

        public virtual ICollection<SaleEntity> Dealings { get; set; }
    }
}
