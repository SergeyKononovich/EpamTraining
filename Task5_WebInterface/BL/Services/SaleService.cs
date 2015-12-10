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
    }
}