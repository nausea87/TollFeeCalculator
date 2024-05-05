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
            int nextFee = vehicle.GetTollFee(date.Hour, date.Minute);
            int tempFee = vehicle.GetTollFee(intervalStart.Hour, intervalStart.Minute);

            TimeSpan timeDiff = date - intervalStart;

            if (timeDiff.TotalMinutes <= 60)
            {
                if (totalFee > 0) totalFee -= tempFee;
                tempFee = Math.Max(tempFee, nextFee);
                totalFee += tempFee;
            }
            else
            {
                totalFee += nextFee;
                intervalStart = date;
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
