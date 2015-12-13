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


        public SalesPage GetPage(SalesFilter filter, SalesSortingOptions sortOpt, int skip, int take)
        {
            var filterBL = Mapper.Map<BL.Models.SaleFilter>(filter);
            var sortOptBL = Mapper.Map<BL.Models.SalesSortingOptions>(sortOpt);

            var salePageBL = Repository.GetPage(filterBL, sortOptBL, skip, take);

            return Mapper.Map<SalesPage>(salePageBL);
        }
    }
}