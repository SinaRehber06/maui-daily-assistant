using Firebase.Database;
using Firebase.Database.Query;
using System.Net.Http.Json;
using MauiOdev3.Models;

namespace MauiOdev3.Services;

public static class FireBaseService
{
    private static readonly string apiKey = "AIzaSyAG_DsfSn4CpEK7ZdkYRZ2ZTObQhx4Z0Iw"; 
    private static readonly HttpClient client = new HttpClient();

    private static readonly FirebaseClient fc = new FirebaseClient("https://mauiodev3-3d8d9-default-rtdb.firebaseio.com/");

    

    public static async Task<(bool success, string message)> Login(string email, string password)
    {
        var url = $"https://identitytoolkit.googleapis.com/v1/accounts:signInWithPassword?key={apiKey}";
        try
        {
            var response = await client.PostAsJsonAsync(url, new { email, password, returnSecureToken = true });
            return response.IsSuccessStatusCode ? (true, "Giriş Başarılı") : (false, "E-posta veya şifre hatalı");
        }
        catch { return (false, "Bağlantı hatası"); }
    }

    public static async Task<(bool success, string message)> Register(string email, string password)
    {
        var url = $"https://identitytoolkit.googleapis.com/v1/accounts:signUp?key={apiKey}";
        try
        {
            var response = await client.PostAsJsonAsync(url, new { email, password, returnSecureToken = true });
            return response.IsSuccessStatusCode ? (true, "Kayıt Başarılı") : (false, "Kayıt yapılamadı");
        }
        catch { return (false, "Bağlantı hatası"); }
    }

    

    public static async Task<List<Gorev>> GetGorevler()
    {
        try
        {
            var list = await fc.Child("Gorevler").OnceAsync<Gorev>();
            return list.Select(item => new Gorev
            {
                Id = item.Key,
                Baslik = item.Object.Baslik,
                Detay = item.Object.Detay,
                Tarih = item.Object.Tarih,
                Saat = item.Object.Saat,
                YapildiMi = item.Object.YapildiMi
            }).ToList();
        }
        catch { return new List<Gorev>(); }
    }

    public static async Task AddOrUpdateGorev(Gorev gorev)
    {
        if (string.IsNullOrEmpty(gorev.Id))
            await fc.Child("Gorevler").PostAsync(gorev); 
        else
            await fc.Child("Gorevler").Child(gorev.Id).PutAsync(gorev); 
    }

    public static async Task DeleteGorev(string id)
    {
        await fc.Child("Gorevler").Child(id).DeleteAsync();  
    }
}