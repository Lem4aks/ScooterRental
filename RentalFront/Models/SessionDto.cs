namespace RentalFront.Models
{
    public class SessionDto
    {
        public DateTime StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public decimal RentalCost { get; set; }
    }
}
