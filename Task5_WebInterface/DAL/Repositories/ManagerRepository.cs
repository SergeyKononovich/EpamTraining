using System;
using System.Linq;
using AutoMapper;
using BL.IDAL.IRepositories;
using Data;

namespace DAL.Repositories
{
    public class ManagerRepository : RepositoryBase<Data.Models.ManagerModel, BL.Models.ManagerModel>, IManagerRepository
    {
        public ManagerRepository(StoreContext context)
            : base(context)
        {
        }


        public BL.Models.ManagerModel FindBySecondName(string secondName)
        {
            if (secondName == null) throw new ArgumentNullException(nameof(secondName));

            var modelBL = Context.Set<Data.Models.ManagerModel>()
                .SingleOrDefault(e => e.SecondName.Equals(secondName));

            return Mapper.Map<BL.Models.ManagerModel>(modelBL);
        }
    }
}