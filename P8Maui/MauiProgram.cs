using Microsoft.Extensions.Logging;

namespace P8Maui;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

		// Base SQLite du module 8
		M8Contacts.M8Setup.ConfigureServices(builder);

#if DEBUG
		builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}
