using Task4_FolderMonitor.BL.Entities;

namespace Task4_FolderMonitor.BL.IDAL.IRepositories
{
    public interface IManagerRepository : IRepository<Manager>
    {
        Manager FindBySecondName(string secondName);
    }
}