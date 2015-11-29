using Task4_FolderMonitor.BL.Entities;
using Task4_FolderMonitor.BL.IDAL.IRepositories;
using Task4_FolderMonitor.Data;
using Task4_FolderMonitor.Data.Entities;

namespace Task4_FolderMonitor.DAL.Repositories
{
    public class SaleRepository : RepositoryBase<SaleEntity, Sale>, ISaleRepository
    {
        public SaleRepository(StoreContext context) 
            : base(context)
        {
        }
    }
}