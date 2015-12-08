using System;
using System.Collections.Generic;
using System.Data.Entity;
using AutoMapper;
using BL.IDAL.IRepositories;
using Data;
using Data.Entities;

namespace DAL.Repositories
{
    public abstract class RepositoryBase<TEntity, T> : IRepository<T>
        where TEntity : class, IEntity
        where T : class
    {
        protected RepositoryBase(StoreContext context)
        {
            if (context == null) throw new ArgumentNullException(nameof(context));

            Context = context;
        }


        protected StoreContext Context { get; }


        public virtual ICollection<T> GetAll()
        {
            return Mapper.Map<DbSet<TEntity>, List<T>>(Context.Set<TEntity>());
        }
        public virtual T Add(T entityBL)
        {
            if (entityBL == null) throw new ArgumentNullException(nameof(entityBL));

            var entity = Mapper.Map<TEntity>(entityBL);
            Context.Set<TEntity>().Add(entity);
            Context.SaveChanges();

            return Mapper.Map<T>(entity);
        }
        public virtual void Delete(T entityBL)
        {
            if (entityBL == null) throw new ArgumentNullException(nameof(entityBL));

            var entity = Mapper.Map<TEntity>(entityBL);
            Context.Set<TEntity>().Remove(entity);
            Context.SaveChanges();
        }
        public virtual void Update(T entityBL, int id)
        {
            if (entityBL == null) throw new ArgumentNullException(nameof(entityBL));
            
            var existing = Context.Set<TEntity>().Find(id);
            if (existing != null)
            {
                var entity = Mapper.Map<TEntity>(entityBL);
                Context.Entry(existing).CurrentValues.SetValues(entity);
                Context.SaveChanges();
            }
        }
        public virtual T FindById(int id)
        {
            var entity = Context.Set<TEntity>().Find(id);

            return Mapper.Map<T>(entity);
        }
    }
}