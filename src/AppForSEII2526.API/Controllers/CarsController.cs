using AppForSEII2526.API.DTOs;
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
    }
}

