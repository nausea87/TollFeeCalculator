using NB.Tolls.Interfaces;

namespace NB.Tolls.Vehicles
{
    public class Car : IVehicle
    {
        public string VehicleType => "Car";

        // Toll fee rules using time ranges and corresponding fees
        private readonly List<(TimeSpan Start, TimeSpan End, int Fee)> tollFeeRules =
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

        // In a real application we'd most likely fetch these time spans + the fee from an API call.
        // Example: Our customer store the time spans and fee values in a 3rd party -> and we do a GET request.
        // This way the customer has control and can edit freely without a new deploy.

        // Same goes for other values like MaxTollFee or enum TollFreeVehicles.

        public int GetTollFee(int hour, int minute, DateTime date)
        {

            if (IsTollFreeDate(date))
            {
                return 0;
            }

            var currentTime = new TimeSpan(hour, minute, 0);

            foreach (var (start, end, fee) in tollFeeRules)
            {
                if (currentTime >= start && currentTime <= end)
                {
                    return fee;
                }
            }

            return 0;
        }

        private static bool IsTollFreeDate(DateTime date) // TODO: Should probably be moved later on as it isn't Car-specific.
        {
            var tollFreeDates2024 = new HashSet<(int Month, int Day)>
        {
            (1, 1), (3, 28), (3, 29), (4, 1), (4, 30), (5, 1), (5, 8),
            (5, 9), (6, 5), (6, 6), (6, 21), (11, 1), (12, 24), (12, 25),
            (12, 26), (12, 31)
        };

            return date.Year == 2024 && tollFreeDates2024.Contains((date.Month, date.Day)) ||
                   date.Month == 7 ||
                   date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday;
        }
    }
}
