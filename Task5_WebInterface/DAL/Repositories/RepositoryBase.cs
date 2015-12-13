using System;
using System.Collections.Generic;
using System.Data.Entity.Core;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Migrations;
using System.Linq;
using AutoMapper;
using BL.IDAL.IRepositories;
using Data;

namespace DAL.Repositories
{
    public abstract class RepositoryBase<TModelData, TModelBL> : IRepository<TModelBL>
        where TModelData : Data.Models.ModelBase, new() 
        where TModelBL : BL.Models.ModelBase
    {
        protected RepositoryBase(StoreContext context)
        {
            if (context == null) throw new ArgumentNullException(nameof(context));

            Context = context;
        }


        protected StoreContext Context { get; }


        public virtual IList<TModelBL> GetAll()
        {
            var a = Context.Set<TModelData>().ToList();
            return Mapper.Map<List<TModelData>, List<TModelBL>>(Context.Set<TModelData>().ToList());
        }
        public virtual void Add(TModelBL modelBL)
        {
            if (modelBL == null) throw new ArgumentNullException(nameof(modelBL));

            var modelData = Mapper.Map<TModelData>(modelBL);
            Context.Set<TModelData>().Add(modelData);
            Context.SaveChanges();
        }
        public virtual void AddOrUpdate(TModelBL modelBL)
        {
            if (modelBL == null) throw new ArgumentNullException(nameof(modelBL));

            var modelData = Mapper.Map<TModelData>(modelBL);
            Context.Set<TModelData>().AddOrUpdate(modelData);
            Context.SaveChanges();
        }
        public virtual void Delete(TModelBL modelBL)
        {
            if (modelBL == null) throw new ArgumentNullException(nameof(modelBL));

            var modelData = Mapper.Map<TModelData>(modelBL);
            Context.Set<TModelData>().Remove(modelData);
            Context.SaveChanges();
        }
        public virtual void Delete(int id)
        {
            var modelData = new TModelData { Id = id };
            Context.Set<TModelData>().Attach(modelData);
            Context.Set<TModelData>().Remove(modelData);
            Context.SaveChanges();
        }
        public virtual void Update(TModelBL modelBL, int id)
        {
            if (modelBL == null) throw new ArgumentNullException(nameof(modelBL));
            
            var existing = Context.Set<TModelData>().Find(id);
            if (existing != null)
            {
                var modelData = Mapper.Map<TModelData>(modelBL);
                Context.Entry(existing).CurrentValues.SetValues(modelData);
                Context.SaveChanges();
            }
        }
        public virtual TModelBL FindById(int id)
        {
            var modelData = Context.Set<TModelData>().Find(id);

            return Mapper.Map<TModelBL>(modelData);
        }
    }
}