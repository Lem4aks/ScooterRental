namespace ScooterService.Models
{
    public class Scooter
    {

        public Scooter(string model)
        {
            Id = Guid.NewGuid();
            Model = model;
            Status = true;
            SessionIds = new List<Guid>();
        }
        public Guid Id { get; set; }
        public string? Model { get; set; }

        public  bool Status { get; set; }

        public List<Guid>? SessionIds { get; set; }

        public static Scooter CreateScooter(string model)
        {
            return new Scooter(model);
        }
    }
}
