namespace P8Maui.M5Films;

public partial class MoviesPage : ContentPage
{
    public MoviesPage()
    {
        InitializeComponent();
    }

    private async void CollectionView_SelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.Count == 0)
            return;

        // Route Shell enregistrée dans M5Setup.RegisterRoutes()
        await Shell.Current.GoToAsync(nameof(MovieDetailPage));

        // Deselect
        ((CollectionView)sender!).SelectedItem = null;
    }
}
