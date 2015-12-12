using System.Collections.Generic;
using UIPart.Models;

namespace UIPart.IBL.IServices
{
    public interface ISaleService : IService<SaleModel>
    {
        ICollection<SaleModel> GetPage(SaleFilter filter, int skip, int take,
           out int totalRecords, out int totalDisplayRecords);
    }
}