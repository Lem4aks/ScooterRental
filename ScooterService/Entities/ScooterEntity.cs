namespace ScooterService.Entities
{
    public class ScooterEntity
    {
        public Guid Id { get; set; }
        public string Model { get; set; }

        public string Status { get; set; }

        public List<Guid> SessionIds { get; set; } = new List<Guid>();
    }
}
