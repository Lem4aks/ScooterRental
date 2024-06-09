
namespace ClientService.Entities
{
    public class ClientEntity
    {
        public Guid Id { get; set; }

        public string userName { get; set; }

        public string password { get; set; }

        public string email { get; set; }

        public List<int> SessionIds { get; set; } = new List<int>();

    }
}
