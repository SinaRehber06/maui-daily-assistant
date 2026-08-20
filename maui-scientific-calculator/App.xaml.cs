namespace goruntu_prog_odev
{

    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(new Appshell());
        }
    }
}
