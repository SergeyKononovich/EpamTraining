using BL.Entities;

namespace BL.IDAL.IRepositories
{
    public interface IGoodsRepository : IRepository<Goods>
    {
        Goods FindByName(string name);
    }
}