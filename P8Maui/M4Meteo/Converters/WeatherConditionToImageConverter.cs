using System.Globalization;
using P8Maui.M4Meteo.Models;

namespace P8Maui.M4Meteo.Converters;

internal class WeatherConditionToImageConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        WeatherType weatherCondition = (WeatherType)value!;

        // Images préfixées m4_ dans Resources/Images
        return weatherCondition switch
        {
            Models.WeatherType.Sunny => ImageSource.FromFile("m4_sunny.png"),
            Models.WeatherType.Cloudy => ImageSource.FromFile("m4_cloud.png"),
            _ => ImageSource.FromFile("m4_question.png")
        };
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) =>
        throw new NotImplementedException();
}
