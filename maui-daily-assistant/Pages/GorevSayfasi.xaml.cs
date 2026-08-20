using MauiOdev3.Models;
using MauiOdev3.Services;

namespace MauiOdev3.Pages;

public partial class GorevSayfasi : ContentPage
{
    public GorevSayfasi() { InitializeComponent(); }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await Yenile();
    }

    private async Task Yenile()
    {
       
        var gorevler = await FireBaseService.GetGorevler();
        GorevlerListesi.ItemsSource = gorevler;
    }

    
    private async void OnEditClicked(object sender, EventArgs e)
    {
        if (sender is Button btn && btn.CommandParameter is Gorev secilenGorev)
        {
           
            await Navigation.PushAsync(new GorevDetaySayfasi(secilenGorev));
        }
    }

    private async void OnDeleteClicked(object sender, EventArgs e)
    {
        if (sender is Button btn && btn.CommandParameter is Gorev gorev)
        {
            
            bool confirm = await DisplayAlert("Silinsin mi?", "Silmeyi onaylıyor musunuz?", "Tamam", "İptal");
            if (confirm)
            {
                await FireBaseService.DeleteGorev(gorev.Id);
                await Yenile();
            }
        }
    }

    private async void OnAddGorevClicked(object sender, EventArgs e)
    {
       
        await Navigation.PushAsync(new GorevDetaySayfasi());
    }

    private async void OnRefreshClicked(object sender, EventArgs e) => await Yenile();
}