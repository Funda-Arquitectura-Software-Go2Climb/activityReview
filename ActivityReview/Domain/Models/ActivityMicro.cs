namespace ActivityReview.ActivityReview.Domain.Models;

public class ActivityMicro
{
    public int id { get; set; }
   
    public String name{ get; set; }
    public String description{ get; set; }
    //yo creo que este es un serviceId
    public int  activityId{ get; set; }
}