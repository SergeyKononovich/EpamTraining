using System;
using System.Collections.Generic;
using System.Data.Entity;
using AutoMapper;
using Task4_FolderMonitor.BL.IDAL;
using Task4_FolderMonitor.Data;

namespace Task4_FolderMonitor.DAL.Repositories
{
    public abstract class RepositoryBase<TEntity, T> : IRepository<T>
        where TEntity : class
        where T : class
    {
        private StoreContext Context { get; }

        public ICollection<T> List
        {
            get { return Mapper.Map<DbSet<TEntity>, List<T>>(Context.Set<TEntity>()); }
        }


        protected RepositoryBase(StoreContext context)
        {
            if (context == null) throw new ArgumentNullException(nameof(context));

            Context = context;
        }
        public void Add(T entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            var model = Mapper.Map<TEntity>(entity);
            Context.Set<TEntity>().Add(model);
            Context.SaveChanges();
        }
        public void Delete(T entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            var model = Mapper.Map<TEntity>(entity);
            Context.Set<TEntity>().Remove(model);
            Context.SaveChanges();
        }
        public T FindById(int id)
        {
            var model = Context.Set<TEntity>().Find(id);

            return Mapper.Map<T>(model);
        }
        public void Update(T updated, int id)
        {
            if (updated == null) throw new ArgumentNullException(nameof(updated));
            
            var existing = Context.Set<TEntity>().Find(id);
            if (existing != null)
            {
                Context.Entry(existing).CurrentValues.SetValues(updated);
                Context.SaveChanges();
            }
        }
    }
}