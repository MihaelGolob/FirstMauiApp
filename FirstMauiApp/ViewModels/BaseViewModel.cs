using System.ComponentModel;
using System.Runtime.CompilerServices;
using CommunityToolkit.Mvvm.ComponentModel;

namespace FirstMauiApp.ViewModels; 

public partial class BaseViewModel : ObservableObject{
    
    public BaseViewModel() {
    }
    
    // private fields
    [ObservableProperty] 
    [NotifyPropertyChangedFor(nameof(IsNotBusy))] 
    private bool _isBusy;
    
    [ObservableProperty]
    private string _title;
    
    // public properties
    public bool IsNotBusy => !IsBusy;
}