using System.Collections.Generic;
using AutoMapper;
using BL.IDAL.IRepositories;
using UIPart.IBL.IServices;
using UIPart.Models;

namespace BL.Services
{
    public class SaleService : ServicesBase<BL.Models.SaleModel, SaleModel, ISaleRepository>, ISaleService
    {
        public SaleService(ISaleRepository repository) 
            : base(repository)
        {
        }


        public ICollection<SaleModel> GetPage(SaleFilter filter, int skip, int take, 
            out int totalRecords, out int totalDisplayRecords)
        {
            var filterBL = Mapper.Map<BL.Models.SaleFilter>(filter);
            var salesBL = Repository.GetPage(filterBL, skip, take, out totalRecords, out totalDisplayRecords);

            return Mapper.Map<ICollection<SaleModel>>(salesBL);
        }
    }
}