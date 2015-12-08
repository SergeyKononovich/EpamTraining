using BL.Entities;
using BL.IDAL.IRepositories;
using Data;
using Data.Entities;

namespace DAL.Repositories
{
    public class SaleRepository : RepositoryBase<SaleEntity, Sale>, ISaleRepository
    {
        public SaleRepository(StoreContext context) 
            : base(context)
        {
        }
    }
}