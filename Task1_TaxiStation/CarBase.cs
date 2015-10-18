using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1_TaxiStation
{
    public abstract class CarBase : ICar
    {
        private string _registrationNumber;
        private ICarSpecifications _specifications;

        public abstract string Brand { get; }
        public abstract string Model { get; }
        public abstract DateTime ReleaseDate { get; }
        public string RegistrationNumber
        {
            get { return _registrationNumber; }
            set
            {
                if (value == null)
                    throw new ArgumentNullException(nameof(RegistrationNumber));

                _registrationNumber = value;
            }
        }
        public ICarSpecifications Specifications
        {
            get { return _specifications; }
            set
            {
                if (value == null)
                    throw new ArgumentNullException(nameof(Specifications));

                _specifications = value;
            }
        }
    }
}
