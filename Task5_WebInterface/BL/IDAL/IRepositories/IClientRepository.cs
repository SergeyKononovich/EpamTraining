using BL.Models;

namespace BL.IDAL.IRepositories
{
    public interface IClientRepository : IRepository<ClientModel>
    {
        ClientModel FindByFullName(string fullName);
    }
}