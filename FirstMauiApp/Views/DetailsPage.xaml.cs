using FirstMauiApp.ViewModels;

namespace FirstMauiApp.Views; 

public partial class DetailsPage {
    public DetailsPage(MonkeyDetailsViewModel viewModel) {
        InitializeComponent();
        BindingContext = viewModel;
    }
}