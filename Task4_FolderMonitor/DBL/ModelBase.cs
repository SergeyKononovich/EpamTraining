using System.ComponentModel.DataAnnotations;

namespace Task4_FolderMonitor.DBL
{
    public abstract class ModelBase
    {
        [Key]
        public int Id { get; set; }
    }
}
