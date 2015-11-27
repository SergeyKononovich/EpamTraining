using System;

namespace Task4_FolderMonitor.BL.IDAL
{
    public interface IDAO
    {
        IClientRepository ClientRepository { get; }  
        IGoodsRepository GoodsRepository { get; } 
        IManagerRepository ManagerRepository { get; } 
        ISaleRepository SaleRepository { get; } 
    }
}