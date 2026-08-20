using MauiOdev3.Services;

namespace MauiOdev3.Pages;

public partial class GirisSayfasi : ContentPage
{
    public GirisSayfasi()
    {
        InitializeComponent();
    }

    private async void OnGirisClicked(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(EmailEntry.Text) || string.IsNullOrWhiteSpace(PasswordEntry.Text))
        {
            await DisplayAlert("Uyarı", "Lütfen tüm alanları doldurun.", "Tamam");
            return;
        }

        var (success, message) = await FireBaseService.Login(EmailEntry.Text, PasswordEntry.Text);

        if (success)
        {
            
            Preferences.Set("KullaniciEmail", EmailEntry.Text);

           
            Application.Current.MainPage = new AppShell();
        }
        else
        {
            await DisplayAlert("Hata", message, "Tamam");
        }
    }

    private async void OnKaydolGitClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new UyeSayfasi());
    }
}