using FirstMauiApp.ViewModels;

namespace FirstMauiApp;

public partial class MainPage : ContentPage {

    public MainPage(MonkeysViewModel monkeysViewModel) {
        InitializeComponent();
        BindingContext = monkeysViewModel;
    }
}