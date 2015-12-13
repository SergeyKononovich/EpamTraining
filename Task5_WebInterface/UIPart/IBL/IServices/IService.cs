using System.Collections.Generic;

namespace UIPart.IBL.IServices
{
    public interface IService<TModelUI> 
        where TModelUI : class
    {
        IList<TModelUI> GetAll();
        void Add(TModelUI modelUI);
        void AddOrUpdate(TModelUI modelUI);
        void Delete(TModelUI modelUI);
        void Delete(int id);
        void DeleteRange(IEnumerable<int> ids);
        void Update(TModelUI modelUI, int id);
        TModelUI FindById(int id);
    }
}
