using AutoMapper;
using ScooterService.Models;
using ScooterService.Entities;

namespace ScooterService
{
    public class DataBaseMappings : Profile
    {
        public DataBaseMappings() 
        { 
            CreateMap<ScooterEntity, Scooter>().ReverseMap();
        }
    }
}
