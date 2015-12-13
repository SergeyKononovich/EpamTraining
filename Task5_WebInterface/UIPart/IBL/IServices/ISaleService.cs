using UIPart.Models;

namespace UIPart.IBL.IServices
{
    public interface ISaleService : IService<SaleModel>
    {
        SalesPage GetPage(SalesFilter filter, SalesSortingOptions sortOpt, int skip, int take);
    }
}