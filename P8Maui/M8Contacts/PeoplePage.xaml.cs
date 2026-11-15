using P8Maui.M8Contacts.Models;

namespace P8Maui.M8Contacts;

public partial class PeoplePage : ContentPage
{
    public PeoplePage()
    {
        InitializeComponent();
    }

    public async void OnNewButtonClicked(object? sender, EventArgs args)
    {
        statusMessage.Text = "";

        await M8Setup.PersonRepo.AddNewPersonAsync(newPerson.Text);
        statusMessage.Text = M8Setup.PersonRepo.StatusMessage;
    }

    public async void OnGetButtonClicked(object? sender, EventArgs args)
    {
        statusMessage.Text = "";

        List<Person> people = await M8Setup.PersonRepo.GetAllPeopleAsync();
        peopleList.ItemsSource = people;
    }
}
