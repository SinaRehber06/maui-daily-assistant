using MauiOdev3.Models;
using MauiOdev3.Services;

namespace MauiOdev3.Pages;

public partial class GorevDetaySayfasi : ContentPage
{
    private Gorev _mevcutGorev;

    public GorevDetaySayfasi(Gorev? gorev = null)
    {
        InitializeComponent();

        _mevcutGorev = gorev ?? new Gorev();

        if (gorev != null)
        {
            BaslikEntry.Text = _mevcutGorev.Baslik;
            DetayEditor.Text = _mevcutGorev.Detay;
            YapildiMiCheckBox.IsChecked = _mevcutGorev.YapildiMi;

            if (DateTime.TryParse(_mevcutGorev.Tarih, out var t))
                GorevTarih.Date = t;

            if (!string.IsNullOrWhiteSpace(_mevcutGorev.Saat) &&
                TimeSpan.TryParse(_mevcutGorev.Saat, out var s))
                GorevSaat.Time = s;
        }
    }

    private async void OnKaydetClicked(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(BaslikEntry.Text))
        {
            await DisplayAlert("Hata", "Başlık boş olamaz", "Tamam");
            return;
        }

        _mevcutGorev.Baslik = BaslikEntry.Text.Trim();
        _mevcutGorev.Detay = DetayEditor.Text?.Trim() ?? string.Empty;
        _mevcutGorev.YapildiMi = YapildiMiCheckBox.IsChecked;

        
        _mevcutGorev.Tarih = $"{GorevTarih.Date:dd.MM.yyyy}";

       
        if (GorevSaat.Time is TimeSpan secilenSaat)
            _mevcutGorev.Saat = $"{secilenSaat.Hours:D2}:{secilenSaat.Minutes:D2}";
        else
            _mevcutGorev.Saat = "00:00";

        await FireBaseService.AddOrUpdateGorev(_mevcutGorev);
        await Navigation.PopAsync();
    }

}