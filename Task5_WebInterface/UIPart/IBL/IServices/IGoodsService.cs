using UIPart.Models;

namespace UIPart.IBL.IServices
{
    public interface IGoodsService : IService<GoodsModel>
    {
        GoodsModel FindByName(string name);
    }
}