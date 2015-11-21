using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace Task4_FolderMonitor.DBL
{
    public class StoreContext : DbContext
    {
        public StoreContext()
            : base("DBConnection")
        {
        }

        public DbSet<ManagerModel> Managers { get; set; }
        public DbSet<ClientModel> Clients { get; set; }
        public DbSet<GoodsModel> Goods { get; set; }
        public DbSet<SaleModel> Sales { get; set; }

        public string CreateDatabaseScript()
        {
            return ((IObjectContextAdapter) this).ObjectContext.CreateDatabaseScript();
        }
    }

}