using NB.Tolls.Interfaces;

namespace NB.Tolls
{
    public partial class TollCalculator
    {
        private const int MaxTollFee = 60;

        public static int CalculateDailyToll(IVehicle vehicle, DateTime[] dates)
        {
            DateTime intervalStart = dates[0];
            int totalFee = 0;

            foreach (DateTime date in dates)
            {
                int nextFee = vehicle.GetTollFee(date.Hour, date.Minute, date);
                int tempFee = vehicle.GetTollFee(intervalStart.Hour, intervalStart.Minute, intervalStart);

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
    }
}