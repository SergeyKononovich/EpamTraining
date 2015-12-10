using BL.Models;

namespace BL.IDAL.IRepositories
{
    public interface IGoodsRepository : IRepository<GoodsModel>
    {
        GoodsModel FindByName(string name);
    }
}