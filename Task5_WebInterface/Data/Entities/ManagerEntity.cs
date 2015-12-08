using System.Collections.Generic;

namespace Data.Entities
{
    public class ManagerEntity : IEntity
    {
        public int Id { get; set; }
        public string SecondName { get; set; }
        public virtual ICollection<SaleEntity> Dealings { get; set; }
    }
}
