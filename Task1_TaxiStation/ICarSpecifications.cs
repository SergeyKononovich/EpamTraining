using System;

namespace Task1_TaxiStation
{
    public enum EngineType
    {
        Electric,
        Gasoline,
        Diesel,
        Gas
    }

    public interface ICarSpecifications : ICloneable
    {
        EngineType EngineType { get; }
        int MaxSpeedKPH { get; set; }
        int NumberOfSeats { get; set; }
        int AverageCostOfKilometerBYB { get; set; }
    }
}
