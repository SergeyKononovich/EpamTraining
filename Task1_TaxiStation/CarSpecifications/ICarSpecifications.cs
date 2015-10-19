using System;

namespace Task1_TaxiStation.CarSpecifications
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
        int MaxSpeedKmPH { get; set; }
        int NumberOfSeats { get; set; }
        int AverageCostOfKilometerBYB { get; set; }
    }
}
