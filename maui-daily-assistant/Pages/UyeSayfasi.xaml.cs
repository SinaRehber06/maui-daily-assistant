using MauiOdev3.Services;

namespace MauiOdev3.Pages;

public partial class UyeSayfasi : ContentPage
{
    public UyeSayfasi()
    {
        InitializeComponent();
    }

    private async void OnRegisterButtonClicked(object sender, EventArgs e)
    {
      
        if (string.IsNullOrWhiteSpace(EmailEntry.Text) || string.IsNullOrWhiteSpace(PasswordEntry.Text))
        {
            await DisplayAlert("Hata", "Eksik bilgi girmeyiniz.", "Tamam");
            return;
        }

        if (PasswordEntry.Text != PasswordConfirmEntry.Text)
        {
            await DisplayAlert("Hata", "Şifreler uyuşmuyor.", "Tamam");
            return;
        }

      
        var (success, message) = await FireBaseService.Register(EmailEntry.Text, PasswordEntry.Text);

        if (success)
        {
            await DisplayAlert("Başarılı", "Kayıt tamamlandı, giriş yapabilirsiniz.", "Tamam");
            await Navigation.PopAsync();
        }
        else
        {
            await DisplayAlert("Hata", message, "Tamam");
        }
    }

    private async void OnCancelClicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }
}