
namespace RentalService.Entities
{
    public class SessionEntity
    {
        public Guid Id { get; set; }
        public Guid ClientId { get; set; }
        public Guid ScooterId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public decimal RentalCost { get; set; }

    }
}
