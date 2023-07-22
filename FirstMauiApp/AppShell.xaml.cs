namespace FirstMauiApp;

public partial class AppShell : Shell {
    public AppShell() {
        InitializeComponent();
        
        Routing.RegisterRoute(nameof(Views.DetailsPage), typeof(Views.DetailsPage));
    }
}