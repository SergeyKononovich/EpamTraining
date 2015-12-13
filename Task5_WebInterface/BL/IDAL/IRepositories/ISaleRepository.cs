using BL.Models;

namespace BL.IDAL.IRepositories
{
    public interface ISaleRepository : IRepository<SaleModel>
    {
        SalesPage GetPage(SaleFilter filter, SalesSortingOptions sortOpt, int skip, int take);
    }
}