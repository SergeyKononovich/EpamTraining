using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Task4_FolderMonitor.BL.Entities;
using Task4_FolderMonitor.BL.IDAL;
using Task4_FolderMonitor.Data;
using Task4_FolderMonitor.Data.Entities;

namespace Task4_FolderMonitor.DAL.Repositories
{
    public class ClientRepository : RepositoryBase<ClientEntity, Client>, IClientRepository
    {
        public ClientRepository(StoreContext context) 
            : base(context)
        {
        }

        public Client FindByFullName(string fullName)
        {
            var entity = Context.Set<ClientEntity>()
                .SingleOrDefault(e => e.FullName.Equals(fullName));

            return Mapper.Map<Client>(entity);
        }
        public async Task<Client> FindByFullNameAsync(string fullName)
        {
            var entity = await Context.Set<ClientEntity>()
                .SingleOrDefaultAsync(e => e.FullName.Equals(fullName));

            return Mapper.Map<Client>(entity);
        }
    }
}