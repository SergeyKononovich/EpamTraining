using BL.Entities;

namespace BL.IDAL.IRepositories
{
    public interface IClientRepository : IRepository<Client>
    {
        Client FindByFullName(string fullName);
    }
}