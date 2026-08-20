namespace MauiOdev3.Models;

public class SehirHavaDurumu
{
   
    public string DisplayName { get; set; } = string.Empty;

   
    public string SearchName { get; set; } = string.Empty;

    
    public string Source => $"https://www.mgm.gov.tr/sunum/tahmin-klasik-5070.aspx?m={SearchName}&basla=1&bitir=5&rC=111&rZ=fff";
}