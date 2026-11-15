namespace P8Maui.M7Astronomie;

public partial class SunrisePage : ContentPage
{
    ILatLongService LatLongService { get; set; }
    public SunrisePage()
    {
        InitializeComponent();
        LatLongService = new LatLongService();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        activityWaiting.IsRunning = true;
        lblError.IsVisible = false;

        // Gestion des erreurs : la page affiche un message au lieu de planter
        string? errorMessage = null;
        try
        {
            var sunriseSunsetData = await GetSunriseSunsetData();
            InitializeUI(sunriseSunsetData.Item1, sunriseSunsetData.Item2, sunriseSunsetData.Item3);
        }
        catch (PermissionException)
        {
            errorMessage = "Location permission is required to display the sunrise and sunset times. Allow it in System Settings > Privacy & Security > Location Services.";
        }
        catch (Exception ex) when (ex is FeatureNotEnabledException || ex is FeatureNotSupportedException)
        {
            errorMessage = "Unable to get your location. Check that Location Services are turned on.";
        }
        catch (HttpRequestException)
        {
            errorMessage = "Unable to reach the sunrise-sunset web service. Check your internet connection.";
        }
        catch (Exception)
        {
            errorMessage = "Unable to get the sunrise and sunset times. Please try again later.";
        }
        finally
        {
            activityWaiting.IsRunning = false;
        }

        if (errorMessage != null)
        {
            lblDate.Text = DateTime.Today.ToString("D");
            lblSunrise.Text = lblDaylight.Text = lblSunset.Text = "-";
            lblError.Text = errorMessage;
            lblError.IsVisible = true;
            await DisplayAlertAsync("Sunrise", errorMessage, "OK");
        }
    }

    async Task<(DateTime, DateTime, TimeSpan)> GetSunriseSunsetData()
    {

        var latLongData = await LatLongService.GetLatLong();
        var sunData = await new SunriseService().GetSunriseSunsetTimes(latLongData.Latitude, latLongData.Longitude);

        var riseTime = sunData.Sunrise.ToLocalTime();
        var setTime = sunData.Sunset.ToLocalTime();
        var span = setTime.TimeOfDay - riseTime.TimeOfDay;
        return (riseTime, setTime, span);
    }

    void InitializeUI(DateTime riseTime, DateTime setTime, TimeSpan span)
    {
        lblDate.Text = DateTime.Today.ToString("D");
        lblSunrise.Text = riseTime.ToString("h:mm tt");
        lblDaylight.Text = $"{span.Hours} hours, {span.Minutes} minutes";
        lblSunset.Text = setTime.ToString("h:mm tt");
    }
}
