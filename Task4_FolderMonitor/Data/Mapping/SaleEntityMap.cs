using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Task4_FolderMonitor.Data.Entities;

namespace Task4_FolderMonitor.Data.Mapping
{
    public class SaleEntityMap : EntityTypeConfiguration<SaleEntity>
    {
        public SaleEntityMap()
        {
            // key  
            HasKey(t => t.Id);

            // fields  
            Property(t => t.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.Date);
            Property(t => t.SellingPrice);

            // table  
            ToTable("Sales");

            // relationship  
            HasRequired(t => t.Manager).WithMany(c => c.Dealings)
                .HasForeignKey(e => e.ManagerId).WillCascadeOnDelete(false);
            HasRequired(t => t.Client).WithMany(c => c.Dealings)
                .HasForeignKey(e => e.ClientId).WillCascadeOnDelete(false);
            HasRequired(t => t.Goods).WithMany(c => c.Dealings)
                .HasForeignKey(e => e.GoodsId).WillCascadeOnDelete(false);
        }
    }
}