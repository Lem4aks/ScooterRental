
using AutoMapper;
using ClientService.Entities;
using ClientService.Models;

namespace ClientService
{
    public class DataBaseMappings : Profile
    {
        public DataBaseMappings() {
            CreateMap<ClientEntity, Client>().ReverseMap();
        }
    }
}
