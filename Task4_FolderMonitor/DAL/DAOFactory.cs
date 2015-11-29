using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task4_FolderMonitor.BL.IDAL;

namespace Task4_FolderMonitor.DAL
{
    public class DAOFactory : IDAOFactory
    {
        public IDAO CreateDAO()
        {
            return new DAO();
        }
    }
}
