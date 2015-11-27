using System.Threading.Tasks;
using Task4_FolderMonitor.BL.Entities;

namespace Task4_FolderMonitor.BL.IDAL
{
    public interface IClientRepository : IRepository<Client>
    {
        Client FindByFullName(string fullName);
        Task<Client> FindByFullNameAsync(string fullName);
    }
}