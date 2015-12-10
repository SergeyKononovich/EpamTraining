using System;
using AutoMapper;
using BL.IDAL.IRepositories;
using UIPart.IBL.IServices;
using UIPart.Models;

namespace BL.Services
{
    public class ClientService : ServicesBase<BL.Models.ClientModel, ClientModel, IClientRepository>, IClientService
    {
        public ClientService(IClientRepository repository) 
            : base(repository)
        {
        }


        public ClientModel FindByFullName(string fullName)
        {
            if (fullName == null) throw new ArgumentNullException(nameof(fullName));

            var modelBL = Repository.FindByFullName(fullName);
            return Mapper.Map<ClientModel>(modelBL);
        }
    }
}