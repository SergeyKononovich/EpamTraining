using Task4_FolderMonitor.BL.Entities;

namespace Task4_FolderMonitor.BL.IDAL.IRepositories
{
    public interface IClientRepository : IRepository<Client>
    {
        Client FindByFullName(string fullName);
    }
}