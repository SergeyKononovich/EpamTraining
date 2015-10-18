using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1_TaxiStation
{
    public abstract class CarSpecificationsBase : ICarSpecifications
    {
        public abstract EngineType EngineType { get; }
        public int MaxSpeedKPH { get; set; }
        public int NumberOfSeats { get; set; } = 1;
        public int AverageCostOfKilometerBYB { get; set; }
    }
}
