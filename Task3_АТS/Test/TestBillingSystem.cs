using System;
using System.Collections.Generic;
using System.Linq;
using Task3_АТS.ATS;
using Task3_АТS.ATS.Station;
using Task3_АТS.BillingSystem;

namespace Task3_АТS.Test
{
    public class TestBillingSystem : IBillingSystem
    {
        private readonly IDictionary<PhoneNumber, ITariff> _tariffsMap;
        private readonly IDictionary<PhoneNumber, bool> _canChangeTariffMap;
        private readonly IDictionary<PhoneNumber, ICollection<CallInfo>> _callsMap;
        private readonly IDictionary<PhoneNumber, int> _debtMap;


        public TestBillingSystem()
        {
            _tariffsMap = new Dictionary<PhoneNumber, ITariff>();
            _canChangeTariffMap = new Dictionary<PhoneNumber, bool>();
            _callsMap = new Dictionary<PhoneNumber, ICollection<CallInfo>>();
            _debtMap = new Dictionary<PhoneNumber, int>();
        }


        public void RegisterStation(IStation station)
        {
            station.ConnectionFailedEvent += ProcessConnectionFailed;
            station.CallEndEvent += ProcessCall;
        }
        public void AddClient(PhoneNumber phoneNumber, ITariff tariff)
        {
            _tariffsMap.Add(phoneNumber, tariff);
            _canChangeTariffMap.Add(phoneNumber, true);
            _callsMap.Add(phoneNumber, new List<CallInfo>());
            _debtMap.Add(phoneNumber, 0);
        }
        public bool TryChangeTariff(PhoneNumber phoneNumber, ITariff newTariff)
        {
            if (_tariffsMap[phoneNumber] == newTariff)
                return false;

            if (_canChangeTariffMap[phoneNumber])
            {
                _tariffsMap[phoneNumber] = newTariff;
                _canChangeTariffMap[phoneNumber] = false;
                return true;
            }

            return false;
        }
        public void StartNewPeriod()
        {
            foreach (var pair in _callsMap)
                _callsMap[pair.Key].Clear();

            foreach (var key in _canChangeTariffMap.Keys.ToList())
                _canChangeTariffMap[key] = true;
        }
        public ICollection<CallInfo> GetStatisticForPeriod(PhoneNumber phoneNumber)
        {
            return _callsMap[phoneNumber];
        }
        public int GetDebt(PhoneNumber phoneNumber)
        {
            return _debtMap[phoneNumber];
        }

        private void ProcessConnectionFailed(object sender, CallInfo call)
        {
            _callsMap[call.Source].Add(call);
            _callsMap[call.Target].Add(call);
        }
        private void ProcessCall(object sender, CallInfo call)
        {
            _callsMap[call.Source].Add(call);
            _callsMap[call.Target].Add(call);

            _debtMap[call.Source] -= _tariffsMap[call.Source].GetCost(call, true);
            _debtMap[call.Target] -= _tariffsMap[call.Target].GetCost(call, false);
        }
    }
}
