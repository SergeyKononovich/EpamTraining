using System.Collections.Generic;

namespace Task4_FolderMonitor.Data.Entities
{
    public class GoodsEntity : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Cost { get; set; }

        public virtual ICollection<SaleEntity> Dealings { get; set; }
    }
}
