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
