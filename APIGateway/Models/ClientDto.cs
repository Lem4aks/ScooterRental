namespace APIGateway.Models
{
    public class ClientDto
    {
        public Guid Id { get; set; }

        public string Token { get; set; }
        public string userName { get; set; }

        public List<Guid> SessionIds { get; set; }
    }
}
