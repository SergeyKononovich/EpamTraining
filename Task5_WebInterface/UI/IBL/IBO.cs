using System;
using WebInterface.IBL.IServices;

namespace WebInterface.IBL
{
    public interface IBO : IDisposable
    {
        IClientService ClientService { get; }
        IGoodsService GoodsService { get; }
        IManagerService ManagerService { get; }
        ISaleService SaleService { get; }
    }
}