using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using BL.IDAL.IRepositories;
using BL.Models;
using Data;

namespace DAL.Repositories
{
    public class SaleRepository : RepositoryBase<Data.Models.SaleModel, BL.Models.SaleModel>, ISaleRepository
    {
        public SaleRepository(StoreContext context) 
            : base(context)
        {
        }


        public ICollection<SaleModel> GetPage(SaleFilter filter, int skip, int take,
            out int totalRecords, out int totalDisplayRecords)
        {
            var f = filter;
            totalRecords = Context.Sales.Count();

            // TODO EF can not convert function IsFiltered
            totalDisplayRecords = Context.Sales.Count(m => 
                m.Id >= f.FromId && m.Id <= f.ToId &&
                m.Date >= f.FromDate && m.Date <= f.ToDate &&
                m.Goods.Name.Contains(f.GoodsNamePart) &&
                m.Manager.SecondName.Contains(f.ManagerSecondNamePart) &&
                m.Client.FullName.Contains(f.ClientFullNamePart) &&
                m.SellingPrice >= f.FromPrice && m.SellingPrice <= f.ToPrice);

            // TODO EF can not convert function IsFiltered
            var salesDAL = Context.Sales.Where(m => 
                m.Id >= f.FromId && m.Id <= f.ToId &&
                m.Date >= f.FromDate && m.Date <= f.ToDate &&
                m.Goods.Name.Contains(f.GoodsNamePart) &&
                m.Manager.SecondName.Contains(f.ManagerSecondNamePart) &&
                m.Client.FullName.Contains(f.ClientFullNamePart) &&
                m.SellingPrice >= f.FromPrice && m.SellingPrice <= f.ToPrice)
                .OrderBy(m => m.Id).Skip(skip).Take(take).ToList();

            return Mapper.Map<ICollection<SaleModel>>(salesDAL);
        }

        // TODO EF can not convert function
        private static bool IsFiltered(Data.Models.SaleModel m, SaleFilter f)
        {
            return m.Id >= f.FromId && m.Id <= f.ToId &&
                   m.Date >= f.FromDate && m.Date <= f.ToDate &&
                   m.Goods.Name.Contains(f.GoodsNamePart) &&
                   m.Manager.SecondName.Contains(f.ManagerSecondNamePart) &&
                   m.Client.FullName.Contains(f.ClientFullNamePart) &&
                   m.SellingPrice >= f.FromPrice && m.SellingPrice <= f.ToPrice;
        }
    }
}