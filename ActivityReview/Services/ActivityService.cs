
using ActivityReview.ActivityReview.Domain.Models;
using ActivityReview.ActivityReview.Domain.Repositories;
using ActivityReview.ActivityReview.Domain.Services;
using ActivityReview.ActivityReview.Domain.Services.Communication;
using ActivityReview.Shared.Persistence.Contexts;

namespace ActivityReview.ActivityReview.Services;

public class ActivityService : IActivityService
{
    private readonly IActivityRepository _activityRepository;
    private readonly ITouristService _touristService;
    private readonly IActivityMicroservice _activityMicroservice;
    private readonly IUnitOfWork _unitOfWork;
    private readonly AppDbContext _context;
    
    public ActivityService(IActivityRepository activityRepository,
        ITouristService touristService, 
        IUnitOfWork unitOfWork,
        IActivityMicroservice activityMicroservice,
        AppDbContext context)
    {
        _activityRepository = activityRepository;
        _unitOfWork = unitOfWork;
        _context = context; 
        _touristService= touristService;
        _activityMicroservice = activityMicroservice;
    }
    
    public async Task<IEnumerable<Activity>> ListAsync()
    {
        var activities = await _activityRepository.ListAsync();

        foreach (var activity in activities)
        {
            try
            {
                var touristInfo = await _touristService.GetServicesById(activity.CustomersId);
                var activityMicroInfo = await _activityMicroservice.GetActivityMicroservicesId(activity.ActivityId);
                activity.TouristInfo = touristInfo;
                activity.ActivityMicroInfo = activityMicroInfo;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching tourist information for activity {activity.ActivityId}: {ex.Message}");
                activity.TouristInfo = null; // Handle error gracefully
            }
        }

        return activities;
        
    }
    
    public async Task<ActivityResponse> SaveAsync(Activity activity)
    {
        try
        {
            await _activityRepository.AddAsync(activity);
            await _unitOfWork.CompleteAsync();
            return new ActivityResponse(activity);
        }
        catch (Exception e)
        {
            return new ActivityResponse($"An error occurred while saving the activity: {e.Message}");
        }
    }

    public async Task<ActivityResponse> UpdateAsync(int activityId, Activity activity)
    {
        var existingActivity = await _activityRepository.FindByIdAsync(activityId);
        if (existingActivity == null)
            return new ActivityResponse("Activity not found");

        existingActivity.Comment = activity.Comment;
        existingActivity.Date = activity.Date;
        existingActivity.Score = activity.Score;
        existingActivity.ActivityId = activity.ActivityId;
        existingActivity.CustomersId = activity.CustomersId;

        try
        {
            _activityRepository.Update(existingActivity);
            await _unitOfWork.CompleteAsync();
            return new ActivityResponse(existingActivity);

        }
        catch (Exception e)
        {
            return new ActivityResponse($"An error occurred while updating the activity: {e.Message}");
        }
    }

  

    public async Task<ActivityResponse> DeleteAsync(int activityId)
        {
            var existingActivity = await _activityRepository.FindByIdAsync(activityId);
            if (existingActivity == null)
                return new ActivityResponse("Activity not found");
            try
            {
                _activityRepository.Remove(existingActivity);
                await _unitOfWork.CompleteAsync();
                return new ActivityResponse(existingActivity);
            }
            catch (Exception e)
            {
                return new ActivityResponse($"An error occurred while deleting the activity: {e.Message}");
            }
        }

    public async Task<Activity> GetByIdAsync(int id)
    {
        var activity = await _activityRepository.FindByIdAsync(id);
        if (activity == null)
        {
            throw new KeyNotFoundException("Activity not found");
        }

        try
        {
            var touristInfo = await _touristService.GetServicesById(activity.CustomersId);
            var activityMicroInfo = await _activityMicroservice.GetActivityMicroservicesId(activity.ActivityId);

            activity.TouristInfo = touristInfo;
            activity.ActivityMicroInfo = activityMicroInfo;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error fetching tourist information: {ex.Message}");
            activity.TouristInfo = null; // Handle error gracefully
            activity.ActivityMicroInfo = null;
        }

        return activity;

    }
}