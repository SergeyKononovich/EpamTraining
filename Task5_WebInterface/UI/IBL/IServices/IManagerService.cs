using WebInterface.Models;

namespace WebInterface.IBL.IServices
{
    public interface IManagerService : IService<ManagerModel>
    {
        ManagerModel FindBySecondName(string secondName);
    }
}