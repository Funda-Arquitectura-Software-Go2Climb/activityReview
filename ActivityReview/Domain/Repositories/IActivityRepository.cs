using ActivityReview.ActivityReview.Domain.Models;

namespace ActivityReview.ActivityReview.Domain.Repositories;

public interface IActivityRepository
{
    Task<IEnumerable<Activity>> ListAsync();
    
    Task AddAsync(Activity activity);
    Task<Activity> FindByIdAsync(int id);
    void Update(Activity activity);
    void Remove(Activity activity);
}