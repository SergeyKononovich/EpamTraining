using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1_TaxiStation
{
    public interface ICar
    {
        string Brand { get; }
        string Model { get; }
        DateTime ReleaseDate { get; }
        string RegistrationNumber { get; set; }
        ICarSpecifications Specifications { get; set; }
    }
}
