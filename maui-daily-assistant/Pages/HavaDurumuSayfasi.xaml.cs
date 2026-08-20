using System.Collections.ObjectModel;
using System.Text.Json;
using MauiOdev3.Models;

namespace MauiOdev3.Pages;

public partial class HavaDurumuSayfasi : ContentPage
{
    public ObservableCollection<SehirHavaDurumu> Sehirler { get; set; } = new();
    string dosyaYolu = Path.Combine(FileSystem.AppDataDirectory, "sehirler_save.json");

    public HavaDurumuSayfasi()
    {
        InitializeComponent();
        VerileriYukle();
        cvHavaDurumu.ItemsSource = Sehirler;
    }

    private void OnSehirEkleClicked(object sender, EventArgs e)
    {
        string orijinalInput = entSehir.Text?.Trim().ToUpper();
        if (string.IsNullOrEmpty(orijinalInput)) return;

        string temizAd = KarakterleriCevir(orijinalInput);

        if (!Sehirler.Any(s => s.SearchName == temizAd))
        {
            Sehirler.Add(new SehirHavaDurumu
            {
                DisplayName = orijinalInput,
                SearchName = temizAd
            });
            VerileriKaydet();
        }
        entSehir.Text = "";
    }

    private void OnSilClicked(object sender, EventArgs e)
    {
        if (sender is Button btn && btn.CommandParameter is SehirHavaDurumu sehir)
        {
            Sehirler.Remove(sehir);
            VerileriKaydet();
        }
    }

    private string KarakterleriCevir(string text)
    {
        return text.Replace("İ", "I").Replace("Ğ", "G").Replace("Ü", "U")
                   .Replace("Ş", "S").Replace("Ö", "O").Replace("Ç", "C");
    }

    private void VerileriKaydet()
    {
        try
        {
            string json = JsonSerializer.Serialize(Sehirler);
            File.WriteAllText(dosyaYolu, json);
        }
        catch { }
    }

    private void VerileriYukle()
    {
        try
        {
            if (File.Exists(dosyaYolu))
            {
                string json = File.ReadAllText(dosyaYolu);
                var liste = JsonSerializer.Deserialize<List<SehirHavaDurumu>>(json);
                if (liste != null)
                {
                    foreach (var s in liste) Sehirler.Add(s);
                }
            }
        }
        catch { }
    }
}