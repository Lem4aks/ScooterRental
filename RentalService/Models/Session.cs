namespace RentalService.Models
{
    public class Session
    {

        public Session(Guid clientId, Guid scooterId, DateTime startTime) 
        {
            Id = Guid.NewGuid();
            ClientId = clientId;
            ScooterId = scooterId;
            StartTime = startTime;
            EndTime = null;
            RentalCost = 0;

        }
        public Guid Id { get; set; }
        public Guid ClientId { get; set; }
        public Guid ScooterId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public decimal RentalCost { get; set; }

        public static Session CreateSession(Guid clientId, Guid scooterId, DateTime startTime)
        {
            return new Session(clientId, scooterId, startTime);
        }
    }
}
