using AppForSEII2526.API.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AppForSEII2526.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<CarsController> _logger;
        public CarsController(ApplicationDbContext context, ILogger<CarsController> logger)
        {
            _context = context;
            _logger = logger;
        }

        //used to enable your controller to access to the database
        private readonly ApplicationDbContext _context;
        //used to log any information when your system is running
        private readonly ILogger<CarsController> _logger;

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

        //[HttpGet]
        //[Route("[action]")]
        //[ProducesResponseType(typeof(decimal), (int)HttpStatusCode.OK)]
        //[ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
        //public async Task<ActionResult> ComputeDivision(decimal op1, decimal op2)
        //{
        //    if (op2 == 0)
        //    {
        //        _logger.LogError($"{DateTime.Now} Exception: op2=0, division by 0");
        //        return BadRequest("op2 must be different from 0");
        //    }
        //    decimal result = decimal.Round(op1 / op2, 2);
        //    return Ok(result);
        //}

    }
}

