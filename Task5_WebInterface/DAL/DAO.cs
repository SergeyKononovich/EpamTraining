﻿using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using BL.IDAL;
using BL.IDAL.IRepositories;
using BL.Models;
using Common.Logging;
using Data;
using DAL.Repositories;

namespace DAL
{
    public class DAO : IDAO
    {
        private static readonly ILog Log = LogManager.GetLogger("DAL");


        static DAO()
        {
            Log.Trace("Start BL models to Data models mapping.");
            Mapper.CreateMap<Data.Models.ClientModel, BL.Models.ClientModel>().ReverseMap();
            Mapper.CreateMap<Data.Models.GoodsModel, BL.Models.GoodsModel>().ReverseMap();
            Mapper.CreateMap<Data.Models.ManagerModel, BL.Models.ManagerModel>().ReverseMap();
            Mapper.CreateMap<Data.Models.SaleModel, BL.Models.SaleModel>()
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
            Log.Trace("DAO start creating.");
            Context = new StoreContext();
            ClientRepository = new ClientRepository(Context);
            GoodsRepository = new GoodsRepository(Context);
            ManagerRepository = new ManagerRepository(Context);
            SaleRepository = new SaleRepository(Context);
            Log.Trace("DAO created.");
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
                Context.Dispose();
            Log.Trace("DAO disposed.");
        }


        private StoreContext Context { get; }

        public IClientRepository ClientRepository { get; }
        public IGoodsRepository GoodsRepository { get; }
        public IManagerRepository ManagerRepository { get; }
        public ISaleRepository SaleRepository { get; }


        public IDictionary<string, int> Top5GoodsBySalesCount()
        {
            var top = (from q in Context.Goods
                        orderby q.Dealings.Count() descending
                        select new { Name = q.Name, Count = q.Dealings.Count}).Take(5).ToList();

            return top.ToDictionary(g => g.Name, g => g.Count);
        }
    }
}