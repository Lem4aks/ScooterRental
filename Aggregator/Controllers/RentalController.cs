using Aggregator.Services;
using Microsoft.AspNetCore.Mvc;

namespace Aggregator.Controllers
{

    public class RentalController : Controller
    {
        private readonly IUserService _userService;
        private readonly IRentalService _rentalService;
        private readonly IScooterService _scooterService;
        public RentalController(IUserService userService, IRentalService rentalService, IScooterService scooterService)
        {
            _userService = userService;
            _rentalService = rentalService;
            _scooterService = scooterService;
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
