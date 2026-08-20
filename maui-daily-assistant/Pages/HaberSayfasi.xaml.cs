using MauiOdev3.Models;
using MauiOdev3.Services;

namespace MauiOdev3.Pages;

public partial class HaberSayfasi : ContentPage
{
    string currentUrl = "https://www.trthaber.com/manset_articles.rss";

    public HaberSayfasi()
    {
        InitializeComponent();
        LoadNews(currentUrl);
    }

    private async void LoadNews(string url)
    {
        Yukleyici.IsRunning = true;
        Yukleyici.IsVisible = true;

        var haberler = await HaberService.GetCategoryNews(url);
        lstHaberler.ItemsSource = haberler;

        Yukleyici.IsRunning = false;
        Yukleyici.IsVisible = false;
    }

    private void OnCategoryClicked(object sender, EventArgs e)
    {
        if (sender is Button btn && btn.CommandParameter is string url)
        {
            currentUrl = url;
            LoadNews(url);
        }
    }

    private async void OnShareClicked(object sender, EventArgs e)
    {
        var haber = (sender as Button).CommandParameter as HaberModel;
        if (haber != null)
        {
            await Share.Default.RequestAsync(new ShareTextRequest { Uri = haber.Link, Title = haber.Title });
        }
    }

    private async void OnHaberSecildi(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.FirstOrDefault() is HaberModel haber)
        {
            lstHaberler.SelectedItem = null;
            await Navigation.PushAsync(new HaberDetaySayfasi(haber));
        }
    }
}