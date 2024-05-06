using NB.Tolls.Interfaces;

namespace NB.Tolls.Vehicles
{
    public class Motorbike : IVehicle
    {
        public string VehicleType => "Motorbike";
        public int GetTollFee(int hour, int minute, DateTime date) => 0;
    }
}
