using UIPart.Models;

namespace UIPart.IBL.IServices
{
    public interface IClientService : IService<ClientModel>
    {
        ClientModel FindByFullName(string fullName);
    }
}