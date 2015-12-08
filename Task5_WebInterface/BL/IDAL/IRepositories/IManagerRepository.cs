using BL.Entities;

namespace BL.IDAL.IRepositories
{
    public interface IManagerRepository : IRepository<Manager>
    {
        Manager FindBySecondName(string secondName);
    }
}