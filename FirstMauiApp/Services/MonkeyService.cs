using System.Net.Http.Json;
using FirstMauiApp.Models;

namespace FirstMauiApp.Services; 

public class MonkeyService {
    private List<Monkey> _monkeyList = new();
    
    HttpClient _httpClient = new();

    public async Task<List<Monkey>> GetMonkeys() {
        if (_monkeyList?.Count > 0)
            return _monkeyList;

        const string url = "https://raw.githubusercontent.com/jamesmontemagno/app-monkeys/master/MonkeysApp/monkeydata.json";
        var response = await _httpClient.GetAsync(url);
        
        if (response.IsSuccessStatusCode) {
            _monkeyList = await response.Content.ReadFromJsonAsync<List<Monkey>>();
        }
        
        return _monkeyList;
    }
}