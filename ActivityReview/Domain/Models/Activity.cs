using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace ActivityReview.ActivityReview.Domain.Models;

public class Activity
{
    public int Id { get; set; }
    
    public string ActivityCode { get; set; }
    public DateTime Date { get; set; }
    public string Comment { get; set; }
    public float Score { get; set; }
    public int ActivityId { get; set; }
    public int CustomersId { get; set; }
    
    [NotMapped]
    //[JsonIgnore]
    public Tourist TouristInfo { get; set; } // Puedes cambiar "object" por el tipo adecuado
    [NotMapped]
    public ActivityMicro ActivityMicroInfo { get; set; } // Puedes cambiar "object" por el tipo adecuado

}