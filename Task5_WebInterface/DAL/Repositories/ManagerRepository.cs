using System.Linq;
using AutoMapper;
using BL.Entities;
using BL.IDAL.IRepositories;
using Data;
using Data.Entities;

namespace DAL.Repositories
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
    }
}