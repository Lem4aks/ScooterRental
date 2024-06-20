
using AutoMapper;
using ClientService.Entities;
using ClientService.Models;

namespace ClientService
{
    public class DataBaseMappings : Profile
    {
        public DataBaseMappings() {
            CreateMap<ClientEntity, Client>()
            .ConstructUsing(src => Client.Create(src.userName, src.password, src.email))
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.SessionIds, opt => opt.MapFrom(src => src.SessionIds ?? new List<Guid>()));

            CreateMap<Client, ClientEntity>();
        }
    }
}
