using System;
using AutoMapper;
using BL.IDAL.IRepositories;
using UIPart.IBL.IServices;
using UIPart.Models;

namespace BL.Services
{
    public class ManagerService : ServicesBase<BL.Models.ManagerModel, ManagerModel, IManagerRepository>, IManagerService
    {
        public ManagerService(IManagerRepository repository) 
            : base(repository)
        {
        }


        public ManagerModel FindBySecondName(string secondName)
        {
            if (secondName == null) throw new ArgumentNullException(nameof(secondName));

            var modelBL = Repository.FindBySecondName(secondName);

            return Mapper.Map<ManagerModel>(modelBL);
        }
    }
}