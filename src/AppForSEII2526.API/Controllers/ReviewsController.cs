using AppForSEII2526.API.DTOs.ReviewDTO;

namespace AppForSEII2526.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewsController : ControllerBase
    {
        //used to enable your controller to access to the database
        private readonly ApplicationDbContext _context;
        //used to log any information when your system is running
        private readonly ILogger<CarsController> _logger;

        public ReviewsController(ApplicationDbContext context, ILogger<CarsController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet]
        [Route("[action]")]
        [ProducesResponseType(typeof(ReviewDetailDTO), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult> GetReview(int id)
        {
            if (_context.Reviews == null)
            {
                _logger.LogError("Error: Reviews table does not exist");
                return NotFound();
            }

            var review = await _context.Reviews
                .Where(r => r.Id == id)
                    .Include(r => r.ReviewItems) //join table ReviewItems
                    .ThenInclude(ri => ri.Car) //then join table Cars
                        .ThenInclude(car => car.Manufacturer) //then join table Manufacturer
                .Select(r => new ReviewDetailDTO(r.Id, r.Created,r.ApplicationUser.UserName,
                    r.Country, (DriverTypes)r.DriverType,
                    r.ReviewItems
                        .Select(ri => new ReviewItemDTO(ri.Car.Id, ri.Car.Model,
                                ri.Car.Manufacturer, ri.Car.Color,
                                ri.Rating, ri.Description)).ToList<ReviewItemDTO>()))
                .FirstOrDefaultAsync();


            if (review == null)
            {
                _logger.LogError($"Error: Review with id {id} does not exist");
                return NotFound();
            }


            return Ok(review);
        }
    }
}
