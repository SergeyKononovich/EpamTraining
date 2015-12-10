using System;
using UIPart.IBL.IServices;

namespace UIPart.IBL
{
    public interface IBO : IDisposable
    {
        IClientService ClientService { get; }
        IGoodsService GoodsService { get; }
        IManagerService ManagerService { get; }
        ISaleService SaleService { get; }
    }
}