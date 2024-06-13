namespace APIGateway.Models
{
    public class Scooter
    {
        public Guid Id { get; set; }
        public string Model { get; set; }

        public bool Status { get; set; }

        public List<Guid> SessionIds { get; set; } = new List<Guid>();
    }
}
