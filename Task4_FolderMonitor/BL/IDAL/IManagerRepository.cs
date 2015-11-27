using System.Threading.Tasks;
using Task4_FolderMonitor.BL.Entities;

namespace Task4_FolderMonitor.BL.IDAL
{
    public interface IManagerRepository : IRepository<Manager>
    {
        Manager FindBySecondName(string secondName);
        Task<Manager> FindBySecondNameAsync(string secondName);
    }
}