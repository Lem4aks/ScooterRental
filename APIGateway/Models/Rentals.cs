namespace APIGateway.Models
{
    public class Rentals
    {
        public string? ScooterModel {  get; set; }
        public DateTime StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public decimal RentalCost { get; set; }
    }
}
