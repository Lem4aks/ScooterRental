namespace RentalFront.Models
{
    public class Rental
    {
        public Guid Id { get; set; }
        public string? ScooterModel { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public decimal RentalCost { get; set; }
    }
}
