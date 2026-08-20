using MauiOdev3.Models;

namespace MauiOdev3.Pages;

public partial class HaberDetaySayfasi : ContentPage
{
    HaberModel haber;

    public HaberDetaySayfasi(HaberModel secilenHaber)
    {
        InitializeComponent();

       
        haber = secilenHaber;

        
        BindingContext = haber;
    }

    
    private async void ShareClicked(object sender, EventArgs e)
    {
        if (haber == null || string.IsNullOrEmpty(haber.Link))
            return;

        
        await Share.Default.RequestAsync(new ShareTextRequest
        {
            Uri = haber.Link,
            Title = haber.Title
        });
    }

    
    private async void OpenInBrowserClicked(object sender, EventArgs e)
    {
        if (haber != null && !string.IsNullOrEmpty(haber.Link))
        {
            await Launcher.Default.OpenAsync(haber.Link);
        }
    }
}