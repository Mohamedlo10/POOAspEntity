namespace P8Maui.M5Films;

public partial class MovieDetailPage : ContentPage
{
    public MovieDetailPage()
    {
        BindingContext = M5Setup.MainViewModel.SelectedMovie;
        InitializeComponent();
    }
}
