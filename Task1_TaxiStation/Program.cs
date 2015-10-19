using System;
using Task1_TaxiStation.CarSpecifications;
using Task1_TaxiStation.Collections;
using static System.Console;

namespace Task1_TaxiStation
{
    class Program
    {
        static void Main(string[] args)
        {
            ExtendedList<ICar> cars = GetCars();
            TaxiStation myTaxiStation = new TaxiStation(cars);

            WriteLine("Before sorting:\n");
            WriteLine(myTaxiStation);

            // Sample
            WriteLine("\nAfter sorting by AverageCostOfKilometerBYB:\n");
            myTaxiStation.SortCars(x => x.Specifications.AverageCostOfKilometerBYB);
            WriteLine(myTaxiStation);

            // Sample
            myTaxiStation.TryAddCar(cars[0]);
            myTaxiStation.TryAddCar(cars[2]);
            WriteLine("\nAfter adding used cars:\n");
            WriteLine(myTaxiStation);
            WriteLine("\nTotal cars cost: " + myTaxiStation.GetTotalCarsCost() + '\n');

            // Sample
            myTaxiStation.TryRemoveCar(cars[0]);
            myTaxiStation.TryRemoveCar(cars[2]);
            WriteLine("\nAfter remove used cars:\n");
            WriteLine(myTaxiStation);
            WriteLine("\nTotal cars cost: " + myTaxiStation.GetTotalCarsCost() + '\n');

            // Sample
            myTaxiStation.TryAddCar(cars[0]);
            myTaxiStation.TryAddCar(cars[2]);
            WriteLine("\nAfter adding removed cars:\n");
            WriteLine(myTaxiStation);
            WriteLine("\nTotal cars cost: " + myTaxiStation.GetTotalCarsCost() + '\n');

            // Sample
            try
            {
                WriteLine("\nSearch car with speed in the range:\n");
                WriteLine("Enter min speed: ");
                string temp = ReadLine() ?? "0";
                int min = int.Parse(temp);
                WriteLine("Enter max speed: ");
                temp = ReadLine() ?? "100";
                int max = int.Parse(temp);
                WriteLine($"First car with speed in the range [{min}, {max}]:");
                ICar car = myTaxiStation.FirstCarOrDefault(TaxiStationHelper.SpeedBetweenPredicate(min, max));
                string ans = car?.ToString() ?? "Not found\n";
                WriteLine(ans);
            }
            catch (FormatException e)
            {
                WriteLine(e.Message);
            }

            ReadKey();
        }

        static ExtendedList<ICar> GetCars()
        {
            var car1Spec = new GasolineCarSpecifications
            {
                AverageCostOfKilometerBYB = 1000,
                EngineCapacityL = 1.8f,
                FuelConsumptionLP100Km = 10,
                MaxSpeedKmPH = 200,
                NumberOfSeats = 5
            };
            Car car1 = new Car("Pegeot", "406", DateTime.Parse("20/07/1998"), "1234AB-7", car1Spec)
            {
                PriceBYB = 30000000
            };

            var car2Spec = new GasolineCarSpecifications
            {
                AverageCostOfKilometerBYB = 850,
                EngineCapacityL = 2.0f,
                FuelConsumptionLP100Km = 7,
                MaxSpeedKmPH = 240,
                NumberOfSeats = 4
            };
            var car2 = new Car("Toyota", "Camry", DateTime.Parse("01/02/2013"), "1120IK-7", car2Spec)
            {
                PriceBYB = 25000000
            };

            var car3Spec = new ElectricCarSpecifications
            {
                AverageCostOfKilometerBYB = 50,
                MaxSpeedKmPH = 260,
                NumberOfSeats = 4,
                FullChargingTime = TimeSpan.FromHours(10)
            };
            var car3 = new Car("Tesla", "Model S", DateTime.Parse("22/06/2012"), "1444IK-7", car3Spec)
            {
                PriceBYB = 399000000
            };

            car3Spec.FullChargingTime = TimeSpan.FromHours(8);
            car3Spec.MaxSpeedKmPH = 270;
            var car4 = new Car("Tesla", "Model S", DateTime.Parse("10/03/2014"), "3632IK-7", car3Spec)
            {
                PriceBYB = 499000000
            };

            return new ExtendedList<ICar> { car1, car2, car3, car4 };
        }
    }
}
