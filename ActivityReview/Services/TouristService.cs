using ActivityReview.ActivityReview.Domain.Models;
using ActivityReview.ActivityReview.Domain.Services;
using Newtonsoft.Json;

namespace ActivityReview.ActivityReview.Services;

public class TouristService : ITouristService
{
    
    private readonly IHttpClientFactory _httpClientFactory;

    public TouristService(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<IEnumerable<Tourist>> GetServices()
    {
        try
        {
            var httpClient = _httpClientFactory.CreateClient();
            var response = await httpClient.GetAsync("http://localhost:3000/tourists");
            response.EnsureSuccessStatusCode();
            var responseData = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<IEnumerable<Tourist>>(responseData);
        }
        catch (Exception ex)
        {
            // Implementar un mecanismo de registro y notificación de errores
            Console.WriteLine($"Error fetching services: {ex.Message}");
            return null;
        }
    }

    public async Task<Tourist> GetServicesById(int servicesId)
    {
        try
        {
            var httpClient = _httpClientFactory.CreateClient();
            var response = await httpClient.GetAsync($"http://localhost:3000/tourists/{servicesId}");
            response.EnsureSuccessStatusCode();
            var responseData = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Tourist>(responseData);
        }
        catch (Exception ex)
        {
            // Implementar un mecanismo de registro y notificación de errores
            Console.WriteLine($"Error fetching tourist information: {ex.Message}");
            return null;
        }
    }
}