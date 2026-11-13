namespace P8Maui;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();

		// Pages de détail ouvertes par navigation (modules 5 et 7)
		M5Films.M5Setup.RegisterRoutes();
		M7Astronomie.M7Setup.RegisterRoutes();
	}
}
