namespace P8Maui.M7Astronomie;

public class LatLongService : ILatLongService
{
    public async Task<(double Latitude, double Longitude)> GetLatLong()
    {
        var latLoc = 0.0;
        var longLoc = 0.0;

        var status = await Permissions.RequestAsync<Permissions.LocationWhenInUse>();
        if (status == PermissionStatus.Granted)
        {
            var request = new GeolocationRequest(GeolocationAccuracy.Default, TimeSpan.FromSeconds(10));
            var location = await Geolocation.GetLocationAsync(request);

            // Position introuvable (délai dépassé, service désactivé) : erreur affichée par la page
            if (location == null)
                throw new FeatureNotEnabledException("Unable to get the current location.");

            latLoc = location.Latitude;
            longLoc = location.Longitude;
        }
        else
        {
            // Autorisation refusée : erreur affichée par la page au lieu de la position (0, 0)
            throw new PermissionException("Location permission was not granted.");
        }
        return (latLoc, longLoc);
    }
}
