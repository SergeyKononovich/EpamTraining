using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1_TaxiStation
{
    public class GasolineCarSpecifications : CarSpecificationsBase
    {
        private float _engineCapacity = 999f;
        private float _fuelConsumptionLp100Km = 0.1f;

        public override EngineType EngineType => EngineType.Gasoline;
        public float EngineCapacityL
        {
            get { return _engineCapacity; }
            set
            {
                if (value < 0.1)
                    throw new ArgumentOutOfRangeException(nameof(EngineCapacity), value, "Should be > 0.1");

                _engineCapacity = value;
            }
        }
        public float FuelConsumptionLP100Km
        {
            get { return _fuelConsumptionLp100Km; }
            set
            {
                if (value < 0.1)
                    throw new ArgumentOutOfRangeException(nameof(EngineCapacity), value, "Should be > 0.1");

                _fuelConsumptionLp100Km = value;
            }
        }


        public override string ToString()
        {
            return base.ToString() +
                   $"Engine capacity: {EngineCapacityL}\n" +
                   $"Fuel consumption (L/100km): {FuelConsumptionLP100Km}\n";
        }
    }
}
