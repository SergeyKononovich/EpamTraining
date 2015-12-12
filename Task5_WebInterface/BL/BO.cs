using System;
using System.Collections.Generic;
using AutoMapper;
using BL.IDAL;
using BL.Services;
using Common.Logging;
using UIPart.IBL;
using UIPart.IBL.IServices;
using UIPart.Models;

namespace BL
{
    public class BO : IBO
    {
        private static readonly ILog Log = LogManager.GetLogger("BL");


        static BO()
        {
            Log.Trace("Start UI models to BL models mapping.");
            Mapper.CreateMap<BL.Models.ClientModel, ClientModel>().ReverseMap();
            Mapper.CreateMap<BL.Models.GoodsModel, GoodsModel>().ReverseMap();
            Mapper.CreateMap<BL.Models.ManagerModel, ManagerModel>().ReverseMap();
            Mapper.CreateMap<BL.Models.SaleModel, SaleModel>().ReverseMap();
            Mapper.CreateMap<BL.Models.SaleFilter, SaleFilter>().ReverseMap();
        }
        public BO(IDAO dao)
        {
            if (dao == null) throw new ArgumentNullException(nameof(dao));
            
            DAO = dao;
            ClientService = new ClientService(dao.ClientRepository);
            GoodsService = new GoodsService(dao.GoodsRepository);
            ManagerService = new ManagerService(dao.ManagerRepository);
            SaleService = new SaleService(dao.SaleRepository);
            Log.Trace("BO created.");
        }
        ~BO()
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
                DAO.Dispose();
            Log.Trace("BO disposed.");
        }


        private IDAO DAO { get; }


        public IClientService ClientService { get; }
        public IGoodsService GoodsService { get; }
        public IManagerService ManagerService { get; }
        public ISaleService SaleService { get; }
    }
}