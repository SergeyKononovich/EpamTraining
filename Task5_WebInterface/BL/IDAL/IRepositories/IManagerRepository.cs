using BL.Models;

namespace BL.IDAL.IRepositories
{
    public interface IManagerRepository : IRepository<ManagerModel>
    {
        ManagerModel FindBySecondName(string secondName);
    }
}