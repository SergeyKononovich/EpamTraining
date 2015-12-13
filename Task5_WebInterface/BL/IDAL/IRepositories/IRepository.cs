using System.Collections.Generic;

namespace BL.IDAL.IRepositories
{
    public interface IRepository<T> 
        where T : class
    {
        IList<T> GetAll();
        void Add(T modelBL);
        void AddOrUpdate(T modelBL);
        void Delete(T modelBL);
        void Delete(int id);
        void Update(T modelBL, int id);
        T FindById(int id);
    }
}
