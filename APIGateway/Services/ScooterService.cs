using APIGateway.Models;

namespace APIGateway.Services
{
    public class ScooterService
    {
        private readonly Scooter _scooter;
        public ScooterService(Scooter scooter)
        {
            _scooter = scooter;
        }
        
        public async Task<List<Scooter>> GetScooterList()
        {
            return null;
        }
    }
}
