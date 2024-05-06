using NB.Tolls.Interfaces;
using NB.Tolls.Vehicles;

namespace NB.Tolls
{
    internal class Program
    {
        static void Main()
        {
            IVehicle car = new Car();
            IVehicle motorbike = new Motorbike();

            DateTime[] dates =
            [
            new (2024, 5, 6, 2, 0, 0), // 0
            new (2024, 5, 7, 8, 0, 0), // 13
            new (2024, 5, 8, 16, 30, 0), // TollFreeDate
            new (2024, 5, 9, 16, 30, 0), // TollFreeDate
            new (2024, 5, 10, 7, 14, 0) // 18
            ];

            foreach (var date in dates)
            {
                string weekday = date.DayOfWeek.ToString();
                string hour = date.ToString("HH:mm");
                Console.WriteLine($"Date: {date:yyyy-MM-dd}, Weekday: {weekday}, Hour: {hour}");
            }

            int carTollFee = TollCalculator.CalculateDailyToll(car, dates);
            int motorbikeFee = TollCalculator.CalculateDailyToll(motorbike, dates);

            Console.WriteLine($"Total Toll Fee for Car: {carTollFee}");
            Console.WriteLine($"Total Toll Fee for Motorbike: {motorbikeFee}"); // Should always be 0.
        }
    }
}
