using ActivityReview.ActivityReview.Domain.Models;

namespace ActivityReview.ActivityReview.Resources;

public class ActivityResource
{
    public int Id { get; set; }
    public string ActivityCode { get; set; }
    public DateTime Date { get; set; }
    public string Comment { get; set; }
    public float Score { get; set; }
    public Tourist TouristInfo { get; set; }
    public ActivityMicro ActivityMicroInfo { get; set; }
    
}