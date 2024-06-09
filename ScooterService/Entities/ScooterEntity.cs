namespace ScooterService.Entities
{
    public class ScooterEntity
    {
        public int Id { get; set; }
        public string Model { get; set; }

        public string Status { get; set; }

        public List<int> SessionIds { get; set; } = new List<int>();
    }
}
