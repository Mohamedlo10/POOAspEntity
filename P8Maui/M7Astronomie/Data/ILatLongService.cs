namespace P8Maui.M7Astronomie;

public interface ILatLongService
{
    Task<(double Latitude, double Longitude)> GetLatLong();
}
