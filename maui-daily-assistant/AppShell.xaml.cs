using MauiOdev3.Pages;

namespace MauiOdev3;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();

        
        Routing.RegisterRoute(nameof(GorevDetaySayfasi), typeof(GorevDetaySayfasi));
    }
}