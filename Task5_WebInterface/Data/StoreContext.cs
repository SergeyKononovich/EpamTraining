using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Reflection;
using Common.Logging;
using Data.Entities;

namespace Data
{
    public class StoreContext : DbContext
    {
        private static readonly ILog Log = LogManager.GetLogger("Data");

        public StoreContext()
            : base("DBConnection")
        {
            Log.Trace("Store context created");
        }


        public DbSet<ManagerEntity> Managers { get; set; }
        public DbSet<ClientEntity> Clients { get; set; }
        public DbSet<GoodsEntity> Goods { get; set; }
        public DbSet<SaleEntity> Sales { get; set; }


        public string CreateDatabaseScript()
        {
            return ((IObjectContextAdapter)this).ObjectContext.CreateDatabaseScript();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Log.Trace("Start model creating");

            var typesToRegister = Assembly.GetExecutingAssembly().GetTypes()
           .Where(type => !string.IsNullOrEmpty(type.Namespace))
           .Where(type => type.BaseType != null && type.BaseType.IsGenericType
                && type.BaseType.GetGenericTypeDefinition() == typeof(EntityTypeConfiguration<>));

            foreach (var type in typesToRegister)
            {
                dynamic configurationInstance = Activator.CreateInstance(type);
                modelBuilder.Configurations.Add(configurationInstance);
            }

            base.OnModelCreating(modelBuilder);
        }
    }

}