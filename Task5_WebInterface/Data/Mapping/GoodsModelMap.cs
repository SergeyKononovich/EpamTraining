using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;
using Data.Models;

namespace Data.Mapping
{
    public class GoodsModelMap : EntityTypeConfiguration<GoodsModel>
    {
        public GoodsModelMap()
        {
            // key  
            HasKey(t => t.Id);

            // fields  
            Property(t => t.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.Name).IsRequired().HasMaxLength(100)
                .HasColumnAnnotation("Index", new IndexAnnotation(
                    new IndexAttribute("ix_goods_name") { IsUnique = true }));
            Property(t => t.Cost);

            // table  
            ToTable("Goods");
        }
    }
}