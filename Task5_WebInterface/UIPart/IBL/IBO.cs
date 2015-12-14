using System;
using UIPart.IBL.IServices;
using UIPart.Models;

namespace UIPart.IBL
{
    public interface IBO : IDisposable
    {
        IClientService ClientService { get; }
        IGoodsService GoodsService { get; }
        IManagerService ManagerService { get; }
        ISaleService SaleService { get; }

        Statistic GetStatistic();
    }
}