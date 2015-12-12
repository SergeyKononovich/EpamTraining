using System.Collections.Generic;
using BL.Models;

namespace BL.IDAL.IRepositories
{
    public interface ISaleRepository : IRepository<SaleModel>
    {
        ICollection<SaleModel> GetPage(SaleFilter filter, int skip, int take,
            out int totalRecords, out int totalDisplayRecords);
    }
}