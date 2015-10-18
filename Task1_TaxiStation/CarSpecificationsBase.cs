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


        protected CarSpecificationsBase() { }
        protected CarSpecificationsBase(ICarSpecifications other)
        {
            MaxSpeedKPH = other.MaxSpeedKPH;
            NumberOfSeats = other.NumberOfSeats;
            AverageCostOfKilometerBYB = other.AverageCostOfKilometerBYB;
        }

        public override string ToString()
        {
            return "Car specifications:\n" +
                   $"-Engine type: {EngineType}\n" +
                   $"-Max speed (KPH): {MaxSpeedKPH}\n" +
                   $"-Number of seats: {NumberOfSeats}\n" +
                   $"-Average cost of kilometer (BYB): {AverageCostOfKilometerBYB}\n";
        }
        public abstract object Clone();
    }
}
