using System;
using System.Collections.Generic;
using Task3_АТS.ATS;
using Task3_АТS.ATS.Station;
using Task3_АТS.ATS.Terminal;
using Task3_АТS.BillingSystem;

namespace Task3_АТS.Test
{
    public class TestProvider
    {
        private readonly IStation _station;
        private readonly IBillingSystem _billingSystem;


        public TestProvider(IStation station, IBillingSystem billingSystem)
        {
            if (station == null) throw new ArgumentNullException(nameof(station));
            if (billingSystem == null) throw new ArgumentNullException(nameof(billingSystem));


            this._station = station;
            this._billingSystem = billingSystem;
            billingSystem.RegisterStation(station);
        }

        public ITerminal SignContract(ITariff tariff)
        {
            ITerminal t1 = _station.GetPreparedTerminal();

            if (t1 != null)
            {
                _billingSystem.AddClient(t1.PhoneNumber, tariff);
            }

            return t1;
        }
        public bool TryChangeTariff(PhoneNumber phoneNumber, ITariff newTariff)
        {
            return _billingSystem.TryChangeTariff(phoneNumber, newTariff);
        }
        public ICollection<CallInfo> GetStatisticForPeriod(PhoneNumber phoneNumber)
        {
            return _billingSystem.GetStatisticForPeriod(phoneNumber);
        }
        public int GetDebt(PhoneNumber phoneNumber)
        {
            return _billingSystem.GetDebt(phoneNumber);
        }
    }
}
