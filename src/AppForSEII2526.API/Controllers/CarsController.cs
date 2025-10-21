using AppForSEII2526.API.DTOs;
using AppForSEII2526.API.DTOs.RentalDTO;
using AppForSEII2526.API.DTOs.ReviewDTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AppForSEII2526.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarsController : ControllerBase
    {
        //used to enable your controller to access to the database
        private readonly ApplicationDbContext _context;
        //used to log any information when your system is running
        private readonly ILogger<CarsController> _logger;

        public CarsController(ApplicationDbContext context, ILogger<CarsController> logger)
        {
            _context = context;
            _logger = logger;
        }

        //Get para select del CU1 - Comprar coches
        [HttpGet]
        [Route("[action]")]
        [ProducesResponseType(typeof(IList<CarForPurchaseDTO>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetCars_ForPurchase(string? color, string? model)
        {
            var cars = await _context.Cars.Include(Car => Car.Model)
                .Where(c =>(c.Color.Contains(color) || (color == null)) && ((c.Model.Name.Contains(model)) || (model == null)))
                .Select(c=>new CarForPurchaseDTO(c.Id, c.Model.Name, c.Color, c.Manufacturer, c.PurchasingPrice))
                .ToListAsync();
            return Ok(cars);
        }

        //Get para select del CU2 - Alquilar coches
        [HttpGet]
        [Route("[action]")]
        [ProducesResponseType(typeof(IList<CarForRentalDTO>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetCars_ForRental(double? rentingPrice, string? model)
        {
            var cars = await _context.Cars.Include(Car => Car.Model)
                .Where(c => (c.RentingPrice <= rentingPrice || (rentingPrice == null)) && ((c.Model.Name.Contains(model)) || (model == null)))
                .Select(c => new CarForRentalDTO(c.Id, c.Model.Name, c.Color, c.Manufacturer, c.FuelType, c.RentingPrice))
                .ToListAsync();
            return Ok(cars);
        }

        //Get para select del CU4 - Reseñar coches
        [HttpGet]
        [Route("[action]")]
        [ProducesResponseType(typeof(IList<CarForReviewDTO>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetCars_ForReview(string? manufacturer, string? fuelType)
        {
            var cars = await _context.Cars.Include(Car => Car.Model)
                .Where(c => (c.Manufacturer.Contains(manufacturer) || (manufacturer == null)) && ((c.FuelType.Contains(fuelType)) || (fuelType == null)))
                .Select(c => new CarForReviewDTO(c.Id, c.Model.Name, c.CarClass, c.Manufacturer, c.FuelType, c.Color))
                .ToListAsync();
            return Ok(cars);
        }
    }
}

