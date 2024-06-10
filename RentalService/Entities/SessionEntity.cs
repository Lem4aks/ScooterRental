
namespace RentalService.Entities
{
    public class SessionEntity
    {
        public Guid Id { get; set; }
        public int ClientId { get; set; }
        public int ScooterId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public decimal RentalCost { get; set; }

    }
}
