using NB.Tolls.Interfaces;

namespace NB.Tolls
{
    public class Car : IVehicle
    {
        public string VehicleType => "Car";

        // Toll fee rules using time ranges and corresponding fees
        private readonly List<(TimeSpan Start, TimeSpan End, int Fee)> tollFeeRules = // Reasonable or overkill? :)
        [
            (new TimeSpan(6, 0, 0), new TimeSpan(6, 29, 59), 8),
            (new TimeSpan(6, 30, 0), new TimeSpan(6, 59, 59), 13),
            (new TimeSpan(7, 0, 0), new TimeSpan(7, 59, 59), 18),
            (new TimeSpan(8, 0, 0), new TimeSpan(8, 29, 59), 13),
            (new TimeSpan(8, 30, 0), new TimeSpan(14, 59, 59), 8),
            (new TimeSpan(15, 0, 0), new TimeSpan(15, 29, 59), 13),
            (new TimeSpan(15, 30, 0), new TimeSpan(16, 59, 59), 18),
            (new TimeSpan(17, 0, 0), new TimeSpan(17, 59, 59), 13),
            (new TimeSpan(18, 0, 0), new TimeSpan(18, 29, 59), 8)
        ];       

        public int GetTollFee(int hour, int minute)
        {
            var currentTime = new TimeSpan(hour, minute, 0);

            foreach (var (Start, End, Fee) in tollFeeRules)
            {
                if (currentTime >= Start && currentTime <= End)
                {
                    return Fee;
                }
            }

            return 0;
        }
    }
}
