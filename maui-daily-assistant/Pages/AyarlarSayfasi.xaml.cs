namespace MauiOdev3.Pages;

public partial class AyarlarSayfasi : ContentPage
{
    public AyarlarSayfasi()
    {
        InitializeComponent();

       
        if (Application.Current != null)
        {
            ThemeSwitch.IsToggled = Application.Current.UserAppTheme == AppTheme.Dark;
        }
    }

    private void OnThemeToggled(object sender, ToggledEventArgs e)
    {
        if (Application.Current != null)
        {
           
            Application.Current.UserAppTheme = e.Value ? AppTheme.Dark : AppTheme.Light;
        }
    }
}