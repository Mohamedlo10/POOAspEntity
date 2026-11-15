namespace P8Maui.M4Meteo;

public partial class WeatherPage : ContentPage
{
    public WeatherPage()
    {
        InitializeComponent();
    }

    private async void btnRefresh_Clicked(object? sender, EventArgs e)
    {
        btnRefresh.IsEnabled = false;
        actIsBusy.IsRunning = true;

        BindingContext = await Services.WeatherServer.GetWeather(txtPostalCode.Text);

        btnRefresh.IsEnabled = true;
        actIsBusy.IsRunning = false;
    }
}
