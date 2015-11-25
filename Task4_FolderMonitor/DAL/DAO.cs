using System;
using AutoMapper;
using Task4_FolderMonitor.BL;
using Task4_FolderMonitor.BL.Entities;
using Task4_FolderMonitor.BL.IDAL;
using Task4_FolderMonitor.Data;
using Task4_FolderMonitor.Data.Entities;
using Task4_FolderMonitor.DAL.Repositories;

namespace Task4_FolderMonitor.DAL
{
    public class DAO : IDAO
    {
        public StoreContext Context { get; }
        public IClientRepository ClientRepository { get; }
        public IGoodsRepository GoodsRepository { get; }
        public IManagerRepository ManagerRepository { get; }
        public ISaleRepository SaleRepository { get; }


        static DAO()
        {
            Mapper.CreateMap<ClientEntity, Client>().ReverseMap();
            Mapper.CreateMap<GoodsEntity, Goods>().ReverseMap();
            Mapper.CreateMap<ManagerEntity, Manager>().ReverseMap();
            Mapper.CreateMap<SaleEntity, Sale>().ReverseMap();
        }
        public DAO()
        {
            Context = new StoreContext();
            ClientRepository = new ClientRepository(Context);
            GoodsRepository = new GoodsRepository(Context);
            ManagerRepository = new ManagerRepository(Context);
            SaleRepository = new SaleRepository(Context);
        }
        ~DAO()
        {
            Dispose(false);
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
                Context?.Dispose();
        }
    }
}