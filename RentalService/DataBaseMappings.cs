using AutoMapper;
using RentalService.Entities;
using RentalService.Models;

namespace RentalService
{
    public class DataBaseMappings : Profile
    {
        public DataBaseMappings()
        {
            CreateMap<SessionEntity, Session>().ReverseMap();
        }
    }
}
