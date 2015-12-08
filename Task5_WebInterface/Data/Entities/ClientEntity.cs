using System.Collections.Generic;

namespace Data.Entities
{
    public class ClientEntity : IEntity
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public virtual ICollection<SaleEntity> Dealings { get; set; }
    }
}
