namespace MauiOdev3.Pages;

public partial class ProfilSayfasi : ContentPage
{
    public ProfilSayfasi()
    {
        InitializeComponent();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        
        string kullaniciEmail = Preferences.Get("KullaniciEmail", "Bilinmeyen Kullanıcı");
        EmailLabel.Text = kullaniciEmail;
    }

    private async void OnLogoutClicked(object sender, EventArgs e)
    {
        bool answer = await DisplayAlert("Çıkış", "Oturumu kapatmak istediğinize emin misiniz?", "Evet", "Hayır");
        if (answer)
        {
           
            Preferences.Remove("KullaniciEmail");

            
            if (Application.Current != null)
            {
                Application.Current.MainPage = new NavigationPage(new GirisSayfasi());
            }
        }
    }
}