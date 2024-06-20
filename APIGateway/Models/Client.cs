namespace APIGateway.Models
{
    public class Client
    {
        public Guid Id { get; set; }

        public string userName { get; set; }

        public string password { get; set; }

        public string email { get; set; }

        public List<Guid> SessionIds { get; set; }
    }
}
