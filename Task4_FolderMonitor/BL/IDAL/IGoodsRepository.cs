using System.Threading.Tasks;
using Task4_FolderMonitor.BL.Entities;

namespace Task4_FolderMonitor.BL.IDAL
{
    public interface IGoodsRepository : IRepository<Goods>
    {
        Goods FindByName(string name);
        Task<Goods> FindByNameAsync(string name);
    }
}