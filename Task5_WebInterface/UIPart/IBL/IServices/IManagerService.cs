using UIPart.Models;

namespace UIPart.IBL.IServices
{
    public interface IManagerService : IService<ManagerModel>
    {
        ManagerModel FindBySecondName(string secondName);
    }
}