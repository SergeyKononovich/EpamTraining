using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;
using Data.Models;

namespace Data.Mapping
{
    public class ManagerModelMap : EntityTypeConfiguration<ManagerModel>
    {
        public ManagerModelMap()
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