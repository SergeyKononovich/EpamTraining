using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;
using Data.Entities;

namespace Data.Mapping
{
    public class ManagerEntityMap : EntityTypeConfiguration<ManagerEntity>
    {
        public ManagerEntityMap()
        {
            // key  
            HasKey(t => t.Id);

            // fields  
            Property(t => t.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.SecondName).IsRequired().HasMaxLength(30)
                .HasColumnAnnotation("Index", new IndexAnnotation(
                    new IndexAttribute("ix_manager_second_name") { IsUnique = true }));

            // table  
            ToTable("Managers");
        }
    }
}