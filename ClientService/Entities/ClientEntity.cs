
using Microsoft.AspNetCore.Identity;

namespace ClientService.Entities
{
    public class ClientEntity
    {
  
        public Guid Id { get; set; }

        public string? userName { get; set; }

        public string? password { get; set; }

        public string? email { get; set; }

        public List<Guid>? SessionIds { get; set; }

 
    }
}
