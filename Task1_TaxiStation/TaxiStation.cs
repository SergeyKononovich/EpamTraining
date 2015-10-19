﻿using System;
using System.Collections.Generic;
using System.Linq;

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
                throw  new ArgumentNullException(nameof(cars));

            _cars = cars.Clone() as IExtendedCollection<ICar>;
        }

        public override string ToString()
        {
            string str = "TaxiStation:\n";

            str += "--Cars:\n";
            foreach (var car in _cars)
                str += car;

            return str;
        }
        public bool TryAddCar(ICar car)
        {
            if (car != null && _cars.FirstOrDefault(x => ReferenceEquals(x, car)) == null)
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
            if (keySelector == null)
                throw new ArgumentNullException(nameof(keySelector));

            var sorted = _cars.OrderBy(keySelector).ToArray();
            _cars.InitWith(sorted);
        }
        public void SortCars<TKey>(Func<ICar, TKey> keySelector, IComparer<TKey> comparer)
        {
            if (keySelector == null)
                throw new ArgumentNullException(nameof(keySelector));

            var sorted = _cars.OrderBy(keySelector, comparer).ToArray();
            _cars.InitWith(sorted);
        }
        public ICar FirstCarOrDefault(Func<ICar, bool> predicate)
        {
            if (predicate == null)
                throw new ArgumentNullException(nameof(predicate));

            return _cars.FirstOrDefault(predicate);
        }
    }
}
