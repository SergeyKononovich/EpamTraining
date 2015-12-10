using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Data.Models;

namespace Data.Mapping
{
    public class SaleModelMap : EntityTypeConfiguration<SaleModel>
    {
        public SaleModelMap()
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