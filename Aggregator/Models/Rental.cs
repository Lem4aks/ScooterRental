namespace Aggregator.Models
{
    public class Rental
    {
        public Guid Id { get; set; }
        public Guid ClientId { get; set; }
        public Guid ScooterId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public decimal RentalCost { get; set; }
        public Rental(Guid id, Guid clientId, Guid scooterId, DateTime startTime, DateTime? endTime, decimal rentalCost)
        {
            Id = id;
            ClientId = clientId;
            ScooterId = scooterId;
            StartTime = startTime;
            EndTime = endTime;
            RentalCost = rentalCost;
        }
    }
}
