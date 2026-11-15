namespace P8Maui.M5Films;

// Routes de navigation du module 5, enregistrées par AppShell
public static class M5Setup
{
    // Remplace App.MainViewModel du projet Learn (App est un fichier partagé)
    public static ViewModels.MovieListViewModel MainViewModel { get; } = CreateMainViewModel();

    public static void RegisterRoutes()
    {
        Routing.RegisterRoute(nameof(MovieDetailPage), typeof(MovieDetailPage));
    }

    // Même initialisation que App.CreateWindow du projet Learn
    private static ViewModels.MovieListViewModel CreateMainViewModel()
    {
        ViewModels.MovieListViewModel mainViewModel = new();
        mainViewModel.RefreshMovies().ContinueWith((s) => { });

        return mainViewModel;
    }
}
