using System;
using BL.IDAL.IRepositories;

namespace BL.IDAL
{
    public interface IDAO : IDisposable
    {
        IClientRepository ClientRepository { get; }  
        IGoodsRepository GoodsRepository { get; } 
        IManagerRepository ManagerRepository { get; } 
        ISaleRepository SaleRepository { get; }
    }
}