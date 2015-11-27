using System.Collections.Generic;
using System.Threading.Tasks;

namespace Task4_FolderMonitor.BL.IDAL
{
    public interface IRepository<T> 
        where T : class
    {
        ICollection<T> GetAll();
        Task<ICollection<T>> GetAllAsync();
        T Add(T entityBL);
        Task<T> AddAsync(T entityBL);
        void Delete(T entityBL);
        Task DeleteAsync(T entityBL);
        void Update(T entityBL, int id);
        Task UpdateAsync(T entityBL, int id);
        T FindById(int id);
        Task<T> FindByIdAsync(int id);
    }
}
