using BL.IDAL.IRepositories;
using Data;

namespace DAL.Repositories
{
    public class SaleRepository : RepositoryBase<Data.Models.SaleModel, BL.Models.SaleModel>, ISaleRepository
    {
        public SaleRepository(StoreContext context) 
            : base(context)
        {
        }
    }
}