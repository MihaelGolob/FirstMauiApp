using System.Collections.ObjectModel;
using System.Diagnostics;
using CommunityToolkit.Mvvm.Input;
using FirstMauiApp.Models;
using FirstMauiApp.Services;

namespace FirstMauiApp.ViewModels; 

public partial class MonkeysViewModel : BaseViewModel {
    // private fields
    private readonly MonkeyService _monkeyService;
    
    // public properties
    public ObservableCollection<Monkey> Monkeys { get; } = new();
    
    public MonkeysViewModel(MonkeyService monkeyService) {
        Title = "Monkey Finder";
        _monkeyService = monkeyService;
    }

    [RelayCommand]
    private async Task GetMonkeysAsync() {
        if (IsBusy) return;

        try {
            IsBusy = true;
            var monkeys = await _monkeyService.GetMonkeys();
            
            Monkeys.Clear();
            foreach (var monkey in monkeys) {
                Monkeys.Add(monkey);
            }
        }
        catch (Exception e) {
            Debug.WriteLine(e);
            await Shell.Current.DisplayAlert("Error!", $"Unable to get monkeys: {e.Message}", "OK");
        }
        finally {
            IsBusy = false;
        }
    }
}