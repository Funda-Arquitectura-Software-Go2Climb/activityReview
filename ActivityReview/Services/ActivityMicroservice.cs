using System.Xml.Schema;
using ActivityReview.ActivityReview.Domain.Models;
using ActivityReview.ActivityReview.Domain.Services;
using Newtonsoft.Json;

namespace ActivityReview.ActivityReview.Services;

public class ActivityMicroservice:IActivityMicroservice
{
  
    private readonly IHttpClientFactory _httpClientFactory;

    public ActivityMicroservice(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<IEnumerable<ActivityMicro>> GetActivityMicroservices()
    {
        try
        {
            var httpClient = _httpClientFactory.CreateClient();
            var response = await httpClient.GetAsync("http://localhost:8084/api/activities");
            response.EnsureSuccessStatusCode();
            var responseData = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<IEnumerable<ActivityMicro>>(responseData);
        }
        catch (Exception ex)
        {
            // Implementar un mecanismo de registro y notificación de errores
            Console.WriteLine($"Error fetching services: {ex.Message}");
            return null;
        }
    }

    public async Task<ActivityMicro> GetActivityMicroservicesId(int servicesId)
    {
        try
        {
            var httpClient = _httpClientFactory.CreateClient();
            var response = await httpClient.GetAsync($"http://localhost:8084/api/activities/{servicesId}");
            response.EnsureSuccessStatusCode();
            var responseData = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<ActivityMicro>(responseData);
        }
        catch (Exception ex)
        {
            // Implementar un mecanismo de registro y notificación de errores
            Console.WriteLine($"Error fetching tourist information: {ex.Message}");
            return null;
        }
    }
}