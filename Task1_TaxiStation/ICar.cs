using System;
using Task1_TaxiStation.CarSpecifications;

namespace Task1_TaxiStation
{
    public interface ICar
    {
        string Brand { get; }
        string Model { get; }
        DateTime ReleaseDate { get; }
        string RegistrationNumber { get; set; }
        long PriceBYB { get; set; }
        ICarSpecifications Specifications { get; set; }
    }
}
