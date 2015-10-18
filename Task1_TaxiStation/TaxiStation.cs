using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace Task1_TaxiStation
{
    public class TaxiStation
    {
        private readonly IExtendedCollection<ICar> _cars;

        public IExtendedCollection<ICar> Cars
        {
            get { return _cars.Clone() as IExtendedCollection<ICar>; }
        }

        
        public TaxiStation(IExtendedCollection<ICar> cars)
        {
            if (cars == null)
                throw new ArgumentNullException(nameof(cars));

            _cars = cars.Clone() as IExtendedCollection<ICar>;
        }

        public bool TryAddCar(ICar car)
        {
            if (car == null)
                throw new ArgumentNullException(nameof(car));

            if (_cars.FirstOrDefault(x => ReferenceEquals(x, car)) == null)
            {
                _cars.Add(car);
                return true;
            }

            return false;
        }
        public bool TryRemoveCar(ICar car)
        {
            return _cars.Remove(car);
        }
        public void SortCars<TKey>(Func<ICar, TKey> keySelector)
        {
            var sorted = _cars.OrderBy(keySelector).ToArray();
            _cars.InitWith(sorted);
        }
        public void SortCars<TKey>(Func<ICar, TKey> keySelector, IComparer<TKey> comparer)
        {
            var sorted = _cars.OrderBy(keySelector, comparer).ToArray();
            _cars.InitWith(sorted);
        }
    }
}
