using System.Linq;
using AutoMapper;
using BL.Entities;
using BL.IDAL.IRepositories;
using Data;
using Data.Entities;

namespace DAL.Repositories
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