using System.Collections.Generic;

namespace Task4_FolderMonitor.Data.Entities
{
    public class ClientEntity
    {
        public int Id { get; set; }
        public string FullName { get; set; }

        public virtual ICollection<SaleEntity> Dealings { get; set; }
    }
}
