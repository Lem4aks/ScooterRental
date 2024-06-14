using Aggregator.Models;
using Microsoft.AspNetCore.Mvc;

namespace Aggregator.Controllers
{
    public class RentalController : Controller
    {
        private readonly User _user;
        private readonly Scooter _scooter;
        public RentalController(User user, Scooter scooter)
        {
            _user = user;
            _scooter = scooter;
        }
        //если что вместо просто ActionResult будет await, я просто набросок сделал
        public ActionResult MakeRental(Guid id_user, Guid id_scooter, DateTime time)
        {
            return View();
        }

        public ActionResult CloseRental(Guid id_rental, DateTime time)
        {
            return View();
        }
    }
}
