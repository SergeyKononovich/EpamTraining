using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;
using Task4_FolderMonitor.Data.Entities;

namespace Task4_FolderMonitor.Data.Mapping
{
    public class ClientEntityMap : EntityTypeConfiguration<ClientEntity>
    {
        public ClientEntityMap()
        {
            // key  
            HasKey(t => t.Id);

            // fields  
            Property(t => t.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.FullName).IsRequired().HasMaxLength(50)
                .HasColumnAnnotation("Index", new IndexAnnotation(
                    new IndexAttribute("ix_client_full_name") { IsUnique = true }));

            // table  
            ToTable("Clients");
        }
    }
}