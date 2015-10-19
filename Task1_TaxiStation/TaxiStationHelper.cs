using System;

namespace Task1_TaxiStation
{
    public static class TaxiStationHelper
    {
        public static Func<ICar, bool> SpeedBetweenPredicate(int min, int max)
        {
            return x => x.Specifications.MaxSpeedKmPH >= min && x.Specifications.MaxSpeedKmPH <= max;
        }
    }
}
