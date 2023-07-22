using System.Collections.ObjectModel;
using System.Diagnostics;
using CommunityToolkit.Mvvm.Input;
using FirstMauiApp.Models;
using FirstMauiApp.Services;

namespace FirstMauiApp.ViewModels; 

public partial class MonkeysViewModel : BaseViewModel {
    // private fields
    private readonly MonkeyService _monkeyService;
    
    private readonly IConnectivity _connectivity;
    private readonly IGeolocation _geolocation;
    
    // public properties
    public ObservableCollection<Monkey> Monkeys { get; } = new();
    
    public MonkeysViewModel(MonkeyService monkeyService, IConnectivity connectivity, IGeolocation geolocation) {
        Title = "Monkey Finder";
        _monkeyService = monkeyService;
        _connectivity = connectivity;
        _geolocation = geolocation;
    }

    [RelayCommand]
    async Task GetClosestMonkeyAsync() {
        if (IsBusy || Monkeys.Count == 0)
            return;

        try {
            var location = await _geolocation.GetLastKnownLocationAsync();
            if (location == null) {
                location = await _geolocation.GetLocationAsync(
                    new GeolocationRequest {
                        DesiredAccuracy = GeolocationAccuracy.Medium,
                        Timeout = TimeSpan.FromSeconds(15)
                    });
            }
            
            if (location == null) {
                await Shell.Current.DisplayAlert("Location issues!", "Unable to get your location", "OK");
                return;
            }
            
            var closestMonkey = Monkeys.MinBy(m => location.CalculateDistance(m.Latitude, m.Longitude, DistanceUnits.Kilometers));

            await Shell.Current.DisplayAlert("Closest monkey", $"The closest monkey to you is the {closestMonkey.Name} which live in {closestMonkey.Location}", "Nice!");
        }
        catch (Exception e) {
            Debug.WriteLine(e);
            await Shell.Current.DisplayAlert("Error!", $"Unable to get closest monkey: {e.Message}", "OK");
        }
    }

    [RelayCommand]
    async Task GoToDetailsAsync(Monkey monkey) {
        if (monkey == null) return;

        await Shell.Current.GoToAsync(nameof(Views.DetailsPage), true, new Dictionary<string, object> {
            {"Monkey", monkey}
        });
    } 

    [RelayCommand]
    private async Task GetMonkeysAsync() {
        if (IsBusy) return;

        try {
            if (_connectivity.NetworkAccess != NetworkAccess.Internet) {
                await Shell.Current.DisplayAlert("Connection issues!", "Check your wifi and check again", "OK");
                return;
            }
            
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