using CommunityToolkit.Mvvm.ComponentModel;
using FirstMauiApp.Models;

namespace FirstMauiApp.ViewModels; 

[QueryProperty("Monkey", "Monkey")]
public partial class MonkeyDetailsViewModel : BaseViewModel {
    public MonkeyDetailsViewModel() {
    }

    [ObservableProperty]
    private Monkey monkey;
}
