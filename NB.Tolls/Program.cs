using NB.Tolls.Interfaces;

namespace NB.Tolls
{
    internal class Program
    {
        static void Main()
        {
            TollCalculator calculator = new TollCalculator();

            IVehicle car = new Car();
            IVehicle motorbike = new Motorbike();

            DateTime[] dates =
            [
                new (2024, 5, 6, 2, 0, 0),
                new (2024, 5, 6, 8, 0, 0),
                new (2024, 5, 6, 16, 30, 0)
            ]; // Some hard-coded dates for now.
               // In a real application these dates should probably/ideally be replaced by current date.

            foreach (var date in dates)
            {
                string weekday = date.DayOfWeek.ToString();
                string hour = date.ToString("HH:mm");
                Console.WriteLine($"Date: {date:yyyy-MM-dd}, Weekday: {weekday}, Hour: {hour}");
            }

            int carTollFee = calculator.CalculateDailyToll(car, dates);
            int motorbikeFee = calculator.CalculateDailyToll(motorbike, dates);

            Console.WriteLine($"Total Toll Fee for Car: {carTollFee}");

            // Motorbike working as some sort of test for now, as it always should be free.
            Console.WriteLine($"Total Toll Fee for Motorbike: {motorbikeFee}");
        }
    }
}
