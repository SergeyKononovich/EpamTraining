using Task4_FolderMonitor.BL.Entities;

namespace Task4_FolderMonitor.BL.IDAL.IRepositories
{
    public interface IGoodsRepository : IRepository<Goods>
    {
        Goods FindByName(string name);
    }
}