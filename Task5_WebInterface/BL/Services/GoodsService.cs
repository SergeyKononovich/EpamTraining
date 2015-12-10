using System;
using AutoMapper;
using BL.IDAL.IRepositories;
using UIPart.IBL.IServices;
using UIPart.Models;

namespace BL.Services
{
    public class GoodsService : ServicesBase<BL.Models.GoodsModel, GoodsModel, IGoodsRepository>, IGoodsService
    {
        public GoodsService(IGoodsRepository repository) 
            : base(repository)
        {
        }


        public GoodsModel FindByName(string name)
        {
            if (name == null) throw new ArgumentNullException(nameof(name));

            var modelBL = Repository.FindByName(name);

            return Mapper.Map<GoodsModel>(modelBL);
        }
    }
}