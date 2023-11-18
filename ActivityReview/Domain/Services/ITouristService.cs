using ActivityReview.ActivityReview.Domain.Models;

namespace ActivityReview.ActivityReview.Domain.Services;

public interface ITouristService
{
    Task<IEnumerable<Tourist>> GetServices();
    Task<Tourist> GetServicesById(int servicesId);
}