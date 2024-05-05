using NB.Tolls.Interfaces;

namespace NB.Tolls
{
    public class Motorbike : IVehicle
    {
        public string VehicleType => "Motorbike";
        public int GetTollFee(int hour, int minute) => 0;
    }
}
