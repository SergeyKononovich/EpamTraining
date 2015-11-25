using Task4_FolderMonitor.BL.Entities;
using Task4_FolderMonitor.BL.IDAL;
using Task4_FolderMonitor.Data;
using Task4_FolderMonitor.Data.Entities;

namespace Task4_FolderMonitor.DAL.Repositories
{
    public class GoodsRepository : RepositoryBase<GoodsEntity, Goods>, IGoodsRepository
    {
        public GoodsRepository(StoreContext context) 
            : base(context)
        {
        }
    }
}