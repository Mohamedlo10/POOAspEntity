using System.Globalization;
using System.Text.Json;

namespace P8Maui.M7Astronomie;

public class SunriseService
{
    const string SunriseSunsetServiceUrl = "https://api.sunrise-sunset.org";

    public async Task<(DateTime Sunrise, DateTime Sunset)> GetSunriseSunsetTimes(double latitude, double longitude)
    {
        // Point décimal quelle que soit la culture (sinon « 14,7 » en français)
        var query = string.Create(CultureInfo.InvariantCulture, $"{SunriseSunsetServiceUrl}/json?lat={latitude}&lng={longitude}&date=today");

        var client = new HttpClient();
        client.DefaultRequestHeaders.Add("Accept", "application/json");
        var json = await client.GetStringAsync(query);

        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        var data = JsonSerializer.Deserialize<SunriseSunsetData>(json, options);

        // Réponse vide ou inattendue du service web
        if (data?.Results == null)
            throw new InvalidOperationException("Invalid response from the sunrise-sunset web service.");

        // Heures renvoyées en anglais (« 5:40:41 AM ») : analyse indépendante de la culture
        return (DateTime.Parse(data.Results.Sunrise, CultureInfo.InvariantCulture),
                DateTime.Parse(data.Results.Sunset, CultureInfo.InvariantCulture));
    }

    class SunriseSunsetData
    {
#pragma warning disable 0649
        // Field is only set via JSON deserialization, so disable warning that the field is never set.
        //public SunriseSunsetResults Results;
        public SunriseSunsetResults? Results { get; set; }
#pragma warning restore 0649
    }

    class SunriseSunsetResults
    {
#pragma warning disable 0649
        // Fields are only set via JSON deserialization, so disable warning that the fields are never set.
        public string Sunrise { get; set; } = string.Empty;
        public string Sunset { get; set; } = string.Empty;
#pragma warning restore 0649
    }
}
