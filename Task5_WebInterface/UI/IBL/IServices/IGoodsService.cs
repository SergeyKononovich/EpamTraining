using WebInterface.Models;

namespace WebInterface.IBL.IServices
{
    public interface IGoodsService : IService<GoodsModel>
    {
        GoodsModel FindByName(string name);
    }
}