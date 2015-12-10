using System;
using System.Linq;
using AutoMapper;
using BL.IDAL.IRepositories;
using Data;
using GoodsModel = BL.Models.GoodsModel;

namespace DAL.Repositories
{
    public class GoodsRepository : RepositoryBase<Data.Models.GoodsModel, GoodsModel>, IGoodsRepository
    {
        public GoodsRepository(StoreContext context) 
            : base(context)
        {
        }


        public GoodsModel FindByName(string name)
        {
            if (name == null) throw new ArgumentNullException(nameof(name));

            var modelData = Context.Set<Data.Models.GoodsModel>()
                .SingleOrDefault(e => e.Name.Equals(name));

            return Mapper.Map<GoodsModel>(modelData);
        }
    }
}