using System;
using AutoMapper;
using Task4_FolderMonitor.BL.Entities;
using Task4_FolderMonitor.BL.IDAL;
using Task4_FolderMonitor.BL.IDAL.IRepositories;
using Task4_FolderMonitor.Data;
using Task4_FolderMonitor.Data.Entities;
using Task4_FolderMonitor.DAL.Repositories;

namespace Task4_FolderMonitor.DAL
{
    public class DAO : IDAO
    {
        private StoreContext Context { get; }
        
        public IClientRepository ClientRepository { get; }
        public IGoodsRepository GoodsRepository { get; }
        public IManagerRepository ManagerRepository { get; }
        public ISaleRepository SaleRepository { get; }


        static DAO()
        {
            Mapper.CreateMap<ClientEntity, Client>().ReverseMap();
            Mapper.CreateMap<GoodsEntity, Goods>().ReverseMap();
            Mapper.CreateMap<ManagerEntity, Manager>().ReverseMap();
            Mapper.CreateMap<SaleEntity, Sale>()
                .ReverseMap()
                .ForMember(s => s.Client, m => m.MapFrom(s => s.Client.Id == 0 ? s.Client : null))
                .ForMember(s => s.ClientId, m => m.MapFrom(s => s.Client.Id))
                .ForMember(s => s.Manager, m => m.MapFrom(s => s.Manager.Id == 0 ? s.Manager : null))
                .ForMember(s => s.ManagerId, m => m.MapFrom(s => s.Manager.Id))
                .ForMember(s => s.Goods, m => m.MapFrom(s => s.Goods.Id == 0 ? s.Goods : null))
                .ForMember(s => s.GoodsId, m => m.MapFrom(s => s.Goods.Id));
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