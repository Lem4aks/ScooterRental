namespace APIGateway.Models
{
    public class ClientDto
    {
        public string userName { get; set; }

        public List<Guid> SessionIds { get; set; }
    }
}
