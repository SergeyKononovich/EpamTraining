using System.Linq;
using AutoMapper;
using BL.Entities;
using BL.IDAL.IRepositories;
using Data;
using Data.Entities;

namespace DAL.Repositories
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
    }
}