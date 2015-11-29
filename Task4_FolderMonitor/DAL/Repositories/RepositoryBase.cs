using System;
using System.Collections.Generic;
using System.Data.Entity;
using AutoMapper;
using Task4_FolderMonitor.BL.IDAL.IRepositories;
using Task4_FolderMonitor.Data;
using Task4_FolderMonitor.Data.Entities;

namespace Task4_FolderMonitor.DAL.Repositories
{
    public abstract class RepositoryBase<TEntity, T> : IRepository<T>
        where TEntity : class, IEntity
        where T : class
    {
        protected StoreContext Context { get; }


        protected RepositoryBase(StoreContext context)
        {
            if (context == null) throw new ArgumentNullException(nameof(context));

            Context = context;
        }
        

        public ICollection<T> GetAll()
        {
            return Mapper.Map<DbSet<TEntity>, List<T>>(Context.Set<TEntity>());
        }
        public T Add(T entityBL)
        {
            if (entityBL == null) throw new ArgumentNullException(nameof(entityBL));
            var entity = Mapper.Map<TEntity>(entityBL);
            Context.Set<TEntity>().Add(entity);
            Context.SaveChanges();

            return Mapper.Map<T>(entity);
        }
        public void Delete(T entityBL)
        {
            if (entityBL == null) throw new ArgumentNullException(nameof(entityBL));

            var entity = Mapper.Map<TEntity>(entityBL);
            Context.Set<TEntity>().Remove(entity);
            Context.SaveChanges();
        }
        public void Update(T entityBL, int id)
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
        public T FindById(int id)
        {
            var entity = Context.Set<TEntity>().Find(id);

            return Mapper.Map<T>(entity);
        }
    }
}