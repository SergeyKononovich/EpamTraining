using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Net.Mail;
using System.Threading.Tasks;
using AutoMapper;
using Task4_FolderMonitor.BL.IDAL;
using Task4_FolderMonitor.Data;
using Task4_FolderMonitor.Data.Entities;

namespace Task4_FolderMonitor.DAL.Repositories
{
    public abstract class RepositoryBase<TEntity, T> : IRepository<T>
        where TEntity : class, IEntity
        where T : class
    {
        protected StoreContext Context { get; }

        public object Locker { get; } = new object();


        protected RepositoryBase(StoreContext context)
        {
            if (context == null) throw new ArgumentNullException(nameof(context));

            Context = context;
        }
        
        public ICollection<T> GetAll()
        {
            return Mapper.Map<DbSet<TEntity>, List<T>>(Context.Set<TEntity>());
        }
        public async Task<ICollection<T>> GetAllAsync()
        {
            var t = Context.Set<TEntity>().ToListAsync();
            var set = await t;
            return Mapper.Map<List<TEntity>, ICollection<T>>(set);
        }
        public T Add(T entityBL)
        {
            if (entityBL == null) throw new ArgumentNullException(nameof(entityBL));

            var entity = Mapper.Map<TEntity>(entityBL);
            var newEntity = Context.Set<TEntity>().Add(entity);
            Context.SaveChanges();
            
            return Mapper.Map<T>(newEntity);
        }
        public async Task<T> AddAsync(T entityBL)
        {
            var entity = Mapper.Map<TEntity>(entityBL);
            var newEntity = Context.Set<TEntity>().Add(entity);
            await Context.SaveChangesAsync();

            return Mapper.Map<T>(newEntity);
        }
        public void Delete(T entityBL)
        {
            if (entityBL == null) throw new ArgumentNullException(nameof(entityBL));

            var entity = Mapper.Map<TEntity>(entityBL);
            Context.Set<TEntity>().Remove(entity);
            Context.SaveChanges();
        }
        public async Task DeleteAsync(T entityBL)
        {
            if (entityBL == null) throw new ArgumentNullException(nameof(entityBL));

            var entity = Mapper.Map<TEntity>(entityBL);
            Context.Set<TEntity>().Remove(entity);
            await Context.SaveChangesAsync();
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
        public async Task UpdateAsync(T entityBL, int id)
        {
            if (entityBL == null) throw new ArgumentNullException(nameof(entityBL));

            var existing = await Context.Set<TEntity>().FindAsync(id);
            if (existing != null)
            {
                var entity = Mapper.Map<TEntity>(entityBL);
                Context.Entry(existing).CurrentValues.SetValues(entity);
                await Context.SaveChangesAsync();
            }
        }
        public T FindById(int id)
        {
            var entity = Context.Set<TEntity>().Find(id);

            return Mapper.Map<T>(entity);
        }
        public async Task<T> FindByIdAsync(int id)
        {
            var entity = await Context.Set<TEntity>().FindAsync(id);

            return Mapper.Map<T>(entity);
        }
    }
}