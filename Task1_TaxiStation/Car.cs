using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1_TaxiStation
{
    public class Car : ICar
    {
        private string _registrationNumber;
        private ICarSpecifications _specifications;

        public string Brand { get; }
        public string Model { get; }
        public DateTime ReleaseDate { get; }
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


        public Car(string brand, string model, DateTime releaseDate, string regNumber, ICarSpecifications spec)
        {
            if (brand == null)
                throw new ArgumentNullException(nameof(brand));
            if (model == null)
                throw new ArgumentNullException(nameof(model));
            if (releaseDate == null)
                throw new ArgumentNullException(nameof(releaseDate));

            Brand = brand;
            Model = model;
            ReleaseDate = releaseDate;
            RegistrationNumber = regNumber;
            Specifications = spec.Clone() as ICarSpecifications;
        }

        public override string ToString()
        {
            return $"Car: {Brand} {Model} {ReleaseDate.Year}\n" +
                   $"Registration number: {RegistrationNumber}\n" +
                   Specifications +
                   "------------------------------------------------------";
        }
        public override int GetHashCode()
        {
            return Brand.GetHashCode() ^ Model.GetHashCode() ^ ReleaseDate.GetHashCode();
        }
    }
}
