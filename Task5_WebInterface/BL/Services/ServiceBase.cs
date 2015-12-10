using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using BL.IDAL.IRepositories;
using BL.Models;
using UIPart.IBL.IServices;

namespace BL.Services
{
    public abstract class ServicesBase<TModelBL, TModelUI, TRepository> : IService<TModelUI>
        where TModelBL : ModelBase
        where TModelUI : class 
        where TRepository : IRepository<TModelBL>
    {
        protected ServicesBase(TRepository repository)
        {
            if (repository == null) throw new ArgumentNullException(nameof(repository));

            Repository = repository;
        }


        protected TRepository Repository { get; }


        public ICollection<TModelUI> GetAll()
        {
            var a = Repository.GetAll().ToList();
            return Mapper.Map<ICollection<TModelBL>, ICollection<TModelUI>>(Repository.GetAll());
        }
        public void Add(TModelUI modelUI)
        {
            if (modelUI == null) throw new ArgumentNullException(nameof(modelUI));

            var modelBL = Mapper.Map<TModelBL>(modelUI);
            Repository.Add(modelBL);
        }
        public void AddOrUpdate(TModelUI modelUI)
        {
            if (modelUI == null) throw new ArgumentNullException(nameof(modelUI));

            var modelBL = Mapper.Map<TModelBL>(modelUI);
            Repository.AddOrUpdate(modelBL);
        }
        public void Delete(TModelUI modelUI)
        {
            if (modelUI == null) throw new ArgumentNullException(nameof(modelUI));

            var modelBL = Mapper.Map<TModelBL>(modelUI);
            Repository.Delete(modelBL);
        }
        public void Update(TModelUI modelUI, int id)
        {
            if (modelUI == null) throw new ArgumentNullException(nameof(modelUI));

            var modelBL = Mapper.Map<TModelBL>(modelUI);
            Repository.Update(modelBL, id);
        }
        public TModelUI FindById(int id)
        {
            var modelBL = Repository.FindById(id);
            return Mapper.Map<TModelUI>(modelBL);
        }
    }
}