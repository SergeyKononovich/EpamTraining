using System;
using System.Linq;
using AutoMapper;
using BL.IDAL.IRepositories;
using Data;
using ClientModel = BL.Models.ClientModel;

namespace DAL.Repositories
{
    public class ClientRepository : RepositoryBase<Data.Models.ClientModel, ClientModel>, IClientRepository
    {
        public ClientRepository(StoreContext context) 
            : base(context)
        {
        }


        public ClientModel FindByFullName(string fullName)
        {
            if (fullName == null) throw new ArgumentNullException(nameof(fullName));

            var modelData = Context.Set<Data.Models.ClientModel>()
                .SingleOrDefault(e => e.FullName.Equals(fullName));

            return Mapper.Map<ClientModel>(modelData);
        }
    }
}