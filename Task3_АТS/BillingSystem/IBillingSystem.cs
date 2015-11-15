using System.Collections.Generic;
using Task3_АТS.ATS;
using Task3_АТS.ATS.Station;

namespace Task3_АТS.BillingSystem
{
    public interface IBillingSystem
    {
        void RegisterStation(IStation station);
        void AddClient(PhoneNumber phoneNumber, ITariff tariff);
        bool TryChangeTariff(PhoneNumber phoneNumber, ITariff newTariff);
        void StartNewPeriod();
        ICollection<CallInfo> GetStatisticForPeriod(PhoneNumber phoneNumber);
        int GetDebt(PhoneNumber phoneNumber);
    }
}
