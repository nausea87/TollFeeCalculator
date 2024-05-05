namespace NB.Tolls.Interfaces
{
    public interface IVehicle
    {
        string VehicleType { get; }
        int GetTollFee(int hour, int minute);
    }
}