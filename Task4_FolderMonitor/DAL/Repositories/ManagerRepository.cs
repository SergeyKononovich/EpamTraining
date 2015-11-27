using System.Data.Entity;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Threading.Tasks;
using AutoMapper;
using Task4_FolderMonitor.BL.Entities;
using Task4_FolderMonitor.BL.IDAL;
using Task4_FolderMonitor.Data;
using Task4_FolderMonitor.Data.Entities;

namespace Task4_FolderMonitor.DAL.Repositories
{
    public class ManagerRepository : RepositoryBase<ManagerEntity, Manager>, IManagerRepository
    {
        public ManagerRepository(StoreContext context)
            : base(context)
        {
        }

        public Manager FindBySecondName(string secondName)
        {
            var entity = Context.Set<ManagerEntity>()
                .SingleOrDefault(e => e.SecondName.Equals(secondName));

            return Mapper.Map<Manager>(entity);
        }
        public async Task<Manager> FindBySecondNameAsync(string secondName)
        {
            var entity = await Context.Set<ManagerEntity>()
                .SingleOrDefaultAsync(e => e.SecondName.Equals(secondName));

            return Mapper.Map<Manager>(entity);
        }
    }
}