using Task4_FolderMonitor.BL.Entities;
using Task4_FolderMonitor.BL.IDAL;
using Task4_FolderMonitor.Data;
using Task4_FolderMonitor.Data.Entities;

namespace Task4_FolderMonitor.DAL.Repositories
{
    public class ManagerRepository : RepositoryBase<ManagerEntity, Manager>, IManagerRepository
    {
        public ManagerRepository(StoreContext context)
            : base(context)
        {
        }
    }
}