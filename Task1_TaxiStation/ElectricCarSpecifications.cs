using System;

namespace Task1_TaxiStation
{
    public class ElectricCarSpecifications : CarSpecificationsBase
    {
        public override EngineType EngineType => EngineType.Electric;
        public TimeSpan FullChargingTime { get; set; }


        public ElectricCarSpecifications() 
            : base() { }
        private ElectricCarSpecifications(ElectricCarSpecifications other)
            : base(other)
        {
            FullChargingTime = other.FullChargingTime;
        }

        public override string ToString()
        {
            return base.ToString() +
                   $"------Full charging time (h): {FullChargingTime.Hours}\n";
        }
        public override object Clone()
        {
            return new ElectricCarSpecifications(this);
        }
    }
}