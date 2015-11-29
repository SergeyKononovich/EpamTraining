using System.Linq;
using AutoMapper;
using Task4_FolderMonitor.BL.Entities;
using Task4_FolderMonitor.BL.IDAL.IRepositories;
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


        public Goods FindByName(string name)
        {
            var entity = Context.Set<GoodsEntity>()
                .SingleOrDefault(e => e.Name.Equals(name));

            return Mapper.Map<Goods>(entity);
        }
    }
}