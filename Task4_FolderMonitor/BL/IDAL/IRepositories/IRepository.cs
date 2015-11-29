using System.Collections.Generic;

namespace Task4_FolderMonitor.BL.IDAL.IRepositories
{
    public interface IRepository<T> 
        where T : class
    {
        ICollection<T> GetAll();
        T Add(T entityBL);
        void Delete(T entityBL);
        void Update(T entityBL, int id);
        T FindById(int id);
    }
}
