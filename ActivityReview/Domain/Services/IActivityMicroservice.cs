using ActivityReview.ActivityReview.Domain.Models;

namespace ActivityReview.ActivityReview.Domain.Services;

public interface IActivityMicroservice
{
    Task<IEnumerable<ActivityMicro>> GetActivityMicroservices();
    Task<ActivityMicro> GetActivityMicroservicesId(int servicesId);
}