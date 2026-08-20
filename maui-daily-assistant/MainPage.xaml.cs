using MauiOdev3.Pages;

namespace MauiOdev3;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
    }

    private async void OnStartClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new GirisSayfasi());
    }
}
