using NB.Tolls.Interfaces;

public partial class TollCalculator
{
    private const int MaxTollFee = 60;

    public int CalculateDailyToll(IVehicle vehicle, DateTime[] dates)
    {
        DateTime intervalStart = dates[0];
        int totalFee = 0;

        foreach (DateTime date in dates)
        {
            int nextFee = GetTollFee(date, vehicle);
            int tempFee = GetTollFee(intervalStart, vehicle);

            TimeSpan timeDiff = date - intervalStart; // Cleaner than the manual millisecond calculation?

            if (timeDiff.TotalMinutes <= MaxTollFee)
            {
                if (totalFee > 0) totalFee -= tempFee;
                tempFee = Math.Max(tempFee, nextFee); // Using Math.Max istället
                totalFee += tempFee;
            }
            else
            {
                totalFee += nextFee;
            }
        }

        return Math.Min(totalFee, MaxTollFee);
    }

    private bool IsTollFreeVehicle(IVehicle vehicle)
    {
        if (vehicle == null) return false;

        string vehicleType = vehicle.VehicleType;

        return vehicleType == TollFreeVehicles.Motorbike.ToString() ||
               vehicleType == TollFreeVehicles.Tractor.ToString() ||
               vehicleType == TollFreeVehicles.Emergency.ToString() ||
               vehicleType == TollFreeVehicles.Diplomat.ToString() ||
               vehicleType == TollFreeVehicles.Foreign.ToString() ||
               vehicleType == TollFreeVehicles.Military.ToString();
    }

    private int GetTollFee(DateTime date, IVehicle vehicle)
    {
        if (IsTollFreeDate(date) || IsTollFreeVehicle(vehicle)) return 0;

        int hour = date.Hour;
        int minute = date.Minute;

        // TODO : Calculate time of day somewhere else. In Car.cs as only cars pay tolls?

        if (hour == 6 && minute <= 29) return 8;
        if (hour == 6 && minute >= 30) return 13;
        if (hour == 7) return 18;
        if (hour == 8 && minute <= 29) return 13;
        if (hour == 8 && minute >= 30 || hour >= 9 && hour <= 14) return 8;
        if (hour == 15 && minute <= 29) return 13;
        if (hour == 15 && minute >= 30 || hour == 16) return 18;
        if (hour == 17) return 13;
        if (hour == 18 && minute <= 29) return 8;

        return 0;
    }

    // Röda dagar
    private bool IsTollFreeDate(DateTime date)
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
