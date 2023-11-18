
using ActivityReview.ActivityReview.Domain.Models;
using ActivityReview.ActivityReview.Resources;
using AutoMapper;

namespace ActivityReview.ActivityReview.Mapping;

public class ModelToResourceProfile : Profile
{
    public ModelToResourceProfile()
    {
        CreateMap<Activity, ActivityResource>();

    }
}