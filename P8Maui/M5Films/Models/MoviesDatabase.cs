namespace P8Maui.M5Films.Models;

internal static class MoviesDatabase
{
    public async static Task<IEnumerable<Movie>> GetMovies()
    {
        // Fichier de données préfixé m5_ dans Resources/Raw
        using Stream stream = await FileSystem.OpenAppPackageFileAsync("m5_data.json");

        System.Text.Json.JsonSerializerOptions options = new()
        {
            PropertyNameCaseInsensitive = true,
        };

        IEnumerable<Movie>? movies = await System.Text.Json.JsonSerializer.DeserializeAsync<List<Movie>>(stream, options);

        return movies ?? [];
    }
}
