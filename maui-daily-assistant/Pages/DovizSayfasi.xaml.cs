using MauiOdev3.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MauiOdev3.Pages;

public partial class DovizSayfasi : ContentPage
{
    private readonly HttpClient client = new();

    public DovizSayfasi()
    {
        InitializeComponent();
        _ = LoadKurlar();
    }

    private async void OnYenileClicked(object sender, EventArgs e)
        => await LoadKurlar();

    private async Task LoadKurlar()
    {
        Yukleyici.IsRunning = true;

        try
        {
            var list = new List<Doviz>();

         
            var json = await client.GetStringAsync("https://finans.truncgil.com/today.json");
            var allData = JObject.Parse(json);

            foreach (var item in allData)
            {
                if (item.Value is not JObject obj)
                    continue;

                string key = item.Key.Trim();
                string upper = key.ToUpperInvariant();

                bool isDolar = upper is "ABD DOLARI" or "USD";
                bool isEuro = upper is "EURO" or "EUR";
                bool isSterlin = upper.Contains("STERLİN") || upper == "GBP";

                bool isCeyrek = upper.Contains("ÇEYREK") || upper.Contains("CEYREK");
                bool isTam = upper.Contains("TAM");
                bool isCumhuriyet = upper.Contains("CUMHURİYET") || upper.Contains("CUMHURIYET");
                bool isGumus = upper.Contains("GÜMÜŞ") || upper.Contains("GUMUS");

                if (!(isDolar || isEuro || isSterlin || isCeyrek || isTam || isCumhuriyet || isGumus))
                    continue;

                string gosterimAdi =
                    isDolar ? "Dolar (USD)" :
                    isEuro ? "Euro (EUR)" :
                    isSterlin ? "Sterlin (GBP)" :
                    isCeyrek ? "Çeyrek Altın" :
                    isCumhuriyet ? "Cumhuriyet Altını" :
                    isTam ? "Tam Altın" :
                    isGumus ? "Gümüş" :
                    key;

                string fark = obj["Degisim"]?.ToString()
                           ?? obj["Değişim"]?.ToString()
                           ?? "0";

                list.Add(new Doviz
                {
                    Tur = gosterimAdi,
                    Alis = obj["Alis"]?.ToString()
                        ?? obj["Alış"]?.ToString()
                        ?? "0",
                    Satis = obj["Satis"]?.ToString()
                         ?? obj["Satış"]?.ToString()
                         ?? "0",
                    Fark = fark,
                    Yon = fark.Contains("-") ? "↓" : "↑"
                });
            }

            
            try
            {
                var cJson = await client.GetStringAsync(
                    "https://api.coingecko.com/api/v3/simple/price" +
                    "?ids=bitcoin,ethereum&vs_currencies=usd&include_24hr_change=true");

                var cData = JsonConvert.DeserializeObject<
                    Dictionary<string, Dictionary<string, double>>>(cJson);

                if (cData is not null)
                {
                    list.Add(new Doviz
                    {
                        Tur = "Bitcoin (BTC)",
                        Alis = cData["bitcoin"]["usd"].ToString("N2"),
                        Satis = cData["bitcoin"]["usd"].ToString("N2"),
                        Fark = cData["bitcoin"]["usd_24h_change"].ToString("F2"),
                        Yon = cData["bitcoin"]["usd_24h_change"] >= 0 ? "↑" : "↓"
                    });

                    list.Add(new Doviz
                    {
                        Tur = "Ethereum (ETH)",
                        Alis = cData["ethereum"]["usd"].ToString("N2"),
                        Satis = cData["ethereum"]["usd"].ToString("N2"),
                        Fark = cData["ethereum"]["usd_24h_change"].ToString("F2"),
                        Yon = cData["ethereum"]["usd_24h_change"] >= 0 ? "↑" : "↓"
                    });
                }
            }
            catch
            {
                
            }

            KurListesi.ItemsSource = null;
            KurListesi.ItemsSource = list;
        }
        catch (Exception ex)
        {
            await DisplayAlert("Hata", "Veriler yüklenemedi: " + ex.Message, "Tamam");
        }
        finally
        {
            Yukleyici.IsRunning = false;
        }
    }
}
