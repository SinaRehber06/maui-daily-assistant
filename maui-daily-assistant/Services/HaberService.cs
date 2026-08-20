using MauiOdev3.Models;
using Newtonsoft.Json.Linq;

namespace MauiOdev3.Services;

public static class HaberService
{
    public static async Task<List<HaberModel>> GetCategoryNews(string rssUrl)
    {
        try
        {
            
            string apiUrl = $"https://api.rss2json.com/v1/api.json?rss_url={rssUrl}";

            using HttpClient client = new();
            var json = await client.GetStringAsync(apiUrl);

            JObject data = JObject.Parse(json);
            JArray items = (JArray)data["items"]!;

            List<HaberModel> list = new();

            foreach (var item in items)
            {
                string? thumb = item["thumbnail"]?.ToString();
                if (string.IsNullOrEmpty(thumb) || !thumb.StartsWith("http"))
                {
                    thumb = item["enclosure"]?["link"]?.ToString();
                }

                list.Add(new HaberModel
                {
                    Title = item["title"]?.ToString() ?? "",
                    Summary = System.Net.WebUtility.HtmlDecode(item["description"]?.ToString() ?? ""),
                    Link = item["link"]?.ToString() ?? "",
                    Date = item["pubDate"]?.ToString() ?? "",
                    Source = "TRT Haber",
                    Image = !string.IsNullOrEmpty(thumb) ? thumb : "https://www.trthaber.com/static/img/logo/trt-haber.png"
                });
            }

            return list;
        }
        catch
        {
            return new List<HaberModel>();
        }
    }
}