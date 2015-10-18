using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task1_TaxiStation;


namespace Task1_TaxiStation
{
    class Program
    {
        static void Main(string[] args)
        {
            ExtendedList<ICar> cars = GetCars();
            TaxiStation myTaxiStation = new TaxiStation(cars);


            Console.WriteLine("Before sorting:\n");
            foreach (var car in myTaxiStation.Cars)
            {
                Console.WriteLine(car);
            }

            Console.WriteLine("\n\nAfter sorting by AverageCostOfKilometerBYB:\n");
            myTaxiStation.SortCars(x => x.Specifications.AverageCostOfKilometerBYB);
            foreach (var car in myTaxiStation.Cars)
            {
                Console.WriteLine(car);
            }
            
            myTaxiStation.TryAddCar(cars[0]);
            myTaxiStation.TryAddCar(cars[2]);
            Console.WriteLine("\n\nAfter adding used cars:\n");
            foreach (var car in myTaxiStation.Cars)
            {
                Console.WriteLine(car);
            }

            myTaxiStation.TryRemoveCar(cars[0]);
            myTaxiStation.TryRemoveCar(cars[2]);
            Console.WriteLine("\n\nAfter remove used cars:\n");
            foreach (var car in myTaxiStation.Cars)
            {
                Console.WriteLine(car);
            }

            myTaxiStation.TryAddCar(cars[0]);
            myTaxiStation.TryAddCar(cars[2]);
            Console.WriteLine("\n\nAfter adding removed cars:\n");
            foreach (var car in myTaxiStation.Cars)
            {
                Console.WriteLine(car);
            }

            Console.ReadKey();
        }

        static ExtendedList<ICar> GetCars()
        {
            var car1Spec = new GasolineCarSpecifications
            {
                AverageCostOfKilometerBYB = 1000,
                EngineCapacityL = 1.8f,
                FuelConsumptionLP100Km = 10,
                MaxSpeedKPH = 200,
                NumberOfSeats = 5
            };
            Car car1 = new Car("Pegeot", "406", DateTime.Parse("20/07/1998"), "1234AB-7", car1Spec);

            var car2Spec = new GasolineCarSpecifications
            {
                AverageCostOfKilometerBYB = 850,
                EngineCapacityL = 2.0f,
                FuelConsumptionLP100Km = 7,
                MaxSpeedKPH = 240,
                NumberOfSeats = 4
            };
            var car2 = new Car("Toyota", "Camry", DateTime.Parse("01/02/2013"), "1120IK-7", car2Spec);

            var car3Spec = new ElectricCarSpecifications
            {
                AverageCostOfKilometerBYB = 50,
                MaxSpeedKPH = 260,
                NumberOfSeats = 4,
                FullChargingTime = TimeSpan.FromHours(10)
            };
            var car3 = new Car("Tesla", "Model S", DateTime.Parse("22/06/2012"), "1444IK-7", car3Spec);

            car3Spec.FullChargingTime = TimeSpan.FromHours(8);
            car3Spec.MaxSpeedKPH = 270;
            var car4 = new Car("Tesla", "Model S", DateTime.Parse("10/03/2014"), "3632IK-7", car3Spec);

            return new ExtendedList<ICar> { car1, car2, car3, car4 };
        }
    }
}
