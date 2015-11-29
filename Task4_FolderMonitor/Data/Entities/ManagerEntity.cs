using System.Collections.Generic;

namespace Task4_FolderMonitor.Data.Entities
{
    public class ManagerEntity : IEntity
    {
        public int Id { get; set; }
        public string SecondName { get; set; }

        public virtual ICollection<SaleEntity> Dealings { get; set; }
    }
}
