namespace P8Maui.M8Contacts;

// Services du module 8 (base SQLite), enregistrés par MauiProgram
public static class M8Setup
{
    // Remplace App.PersonRepo du module : le singleton est lu dans le conteneur de services
    public static PersonRepository PersonRepo =>
        IPlatformApplication.Current!.Services.GetRequiredService<PersonRepository>();

    public static void ConfigureServices(MauiAppBuilder builder)
    {
        string dbPath = FileAccessHelper.GetLocalFilePath("people.db3");
        builder.Services.AddSingleton<PersonRepository>(s => ActivatorUtilities.CreateInstance<PersonRepository>(s, dbPath));
    }
}
