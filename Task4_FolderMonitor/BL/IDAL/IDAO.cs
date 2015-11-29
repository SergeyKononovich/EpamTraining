using System;
using Task4_FolderMonitor.BL.IDAL.IRepositories;

namespace Task4_FolderMonitor.BL.IDAL
{
    public interface IDAO : IDisposable
    {
        IClientRepository ClientRepository { get; }  
        IGoodsRepository GoodsRepository { get; } 
        IManagerRepository ManagerRepository { get; } 
        ISaleRepository SaleRepository { get; }
    }
}