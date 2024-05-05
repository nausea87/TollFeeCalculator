using NB.Tolls.Interfaces;

namespace NB.Tolls
{
    public class Car : IVehicle
    {
        public string VehicleType => "Car";
        public int GetTollFee(int hour, int minute)
        {
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
    }
}

// Add logics from GetTollFee here instead? To avoid those hard coded numbers...
// Date Time Span calcs?


//06:00–06:29     8 kr
//06:30–06:59     13 kr
//07:00–07:59     18 kr
//08:00–08:29     13 kr
//08:30–14:59     8 kr
//15:00–15:29     13 kr
//15:30–16:59     18 kr
//17:00–17:59     13 kr
//18:00–18:29     8 kr
//18:30–05:59     0 kr

