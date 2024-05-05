using NB.Tolls.Interfaces;

namespace NB.Tolls
{
    public class Car : IVehicle
    {
        public string VehicleType => "Car";
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

