using APIGateway.Models;
using AutoMapper;
using RentalSession;
using ScooterInventoryGrpc;

namespace APIGateway
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ScooterMessage, Scooter>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.Parse(src.Id)))
                .ForMember(dest => dest.SessionIds, opt => opt.MapFrom(src => src.SessionIds.Select(Guid.Parse).ToList()));
            CreateMap<SessionMessage, Session>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.Parse(src.Id)))
            .ForMember(dest => dest.ClientId, opt => opt.MapFrom(src => Guid.Parse(src.ClientId)))
            .ForMember(dest => dest.ScooterId, opt => opt.MapFrom(src => Guid.Parse(src.ScooterId)))
            .ForMember(dest => dest.StartTime, opt => opt.MapFrom(src => DateTime.Parse(src.StartTime)))
            .ForMember(dest => dest.EndTime, opt => opt.MapFrom(src => string.IsNullOrEmpty(src.EndTime) ? (DateTime?)null : DateTime.Parse(src.EndTime)))
            .ForMember(dest => dest.RentalCost, opt => opt.MapFrom(src => Convert.ToDecimal(src.RentalCost)));
        }
    }
}
