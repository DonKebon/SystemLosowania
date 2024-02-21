namespace SystemLosowania
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(Views.ClassPage), typeof(Views.ClassPage));
        }
    }
}