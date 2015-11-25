using System.Collections.Generic;

namespace Task4_FolderMonitor.BL.IDAL
{
    public interface IRepository<T> 
        where T : class
    {
        ICollection<T> List { get; }
        void Add(T entity);
        void Delete(T entity);
        void Update(T updated, int id);
        T FindById(int id);
    }
}
