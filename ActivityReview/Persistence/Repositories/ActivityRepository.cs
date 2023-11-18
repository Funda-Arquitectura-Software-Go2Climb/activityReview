using ActivityReview.ActivityReview.Domain.Models;
using ActivityReview.ActivityReview.Domain.Repositories;
using ActivityReview.Shared.Persistence.Contexts;
using ActivityReview.Shared.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ActivityReview.ActivityReview.Persistence.Repositories;

public class ActivityRepository : BaseRepository,IActivityRepository
{
    public ActivityRepository(AppDbContext context) : base(context)
    {
    }

   public async Task<IEnumerable<Activity>> ListAsync()
    {
        return await _context.Activities.ToListAsync();
    }
   public async Task AddAsync(Activity activity)
    {
        await _context.Activities.AddAsync(activity);
    }
   
   public async Task<Activity> FindByIdAsync(int id)
    {
        return await _context.Activities.FindAsync(id);
    }

 

    public  void Update(Activity activity)
    {
        _context.Activities.Update(activity);
    }

    public  void Remove(Activity activity)
    {
        _context.Activities.Remove(activity);
    }
}