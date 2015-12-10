using System.Collections.Generic;

namespace UIPart.IBL.IServices
{
    public interface IService<TModelUI> 
        where TModelUI : class
    {
        ICollection<TModelUI> GetAll();
        void Add(TModelUI modelUI);
        void AddOrUpdate(TModelUI modelUI);
        void Delete(TModelUI modelUI);
        void Update(TModelUI modelUI, int id);
        TModelUI FindById(int id);
    }
}
