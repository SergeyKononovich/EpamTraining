using WebInterface.Models;

namespace WebInterface.IBL.IServices
{
    public interface IClientService : IService<ClientModel>
    {
        ClientModel FindByFullName(string fullName);
    }
}