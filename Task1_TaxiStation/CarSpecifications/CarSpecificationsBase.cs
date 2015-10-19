namespace Task1_TaxiStation.CarSpecifications
{
    public abstract class CarSpecificationsBase : ICarSpecifications
    {
        public abstract EngineType EngineType { get; }
        public int MaxSpeedKmPH { get; set; }
        public int NumberOfSeats { get; set; } = 1;
        public int AverageCostOfKilometerBYB { get; set; }


        protected CarSpecificationsBase() { }
        protected CarSpecificationsBase(ICarSpecifications other)
        {
            MaxSpeedKmPH = other.MaxSpeedKmPH;
            NumberOfSeats = other.NumberOfSeats;
            AverageCostOfKilometerBYB = other.AverageCostOfKilometerBYB;
        }

        public override string ToString()
        {
            return "----Car specifications:\n" +
                   $"------Engine type: {EngineType}\n" +
                   $"------Max speed: {MaxSpeedKmPH} km/h\n" +
                   $"------Number of seats: {NumberOfSeats}\n" +
                   $"------Average cost of kilometer: {AverageCostOfKilometerBYB} BYB\n";
        }
        public abstract object Clone();
    }
}
