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
        [ProducesResponseType(typeof(DTOs.ReviewDTO.ReviewDetailDTO), (int)HttpStatusCode.OK)]
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

        [HttpPost]
        [Route("[action]")]
        [ProducesResponseType(typeof(ReviewDetailDTO), (int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(ValidationProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.Conflict)]
        public async Task<ActionResult> CreateReview(ReviewForCreateDTO reviewForCreate)
        {
            //any validation defined in ReviewForCreate is checked before running the method so they don't have to be checked again
            if (reviewForCreate.ReviewItems.Count == 0)
                ModelState.AddModelError("ReviewItems", "Error! You must include at least one car to be reviewed");

            // if (!_context.ApplicationUsers.Any(au=>au.UserName==reviewForCreate.CustomerUserName))
            var user = _context.ApplicationUsers.FirstOrDefault(au => au.UserName == reviewForCreate.UserName);
            if (user == null)
                ModelState.AddModelError("ReviewApplicationUser", "Error! UserName is not registered");

            if (ModelState.ErrorCount > 0)
                return BadRequest(new ValidationProblemDetails(ModelState));


            var carIds = reviewForCreate.ReviewItems.Select(ri => ri.CarId).ToList();

            var cars = _context.Cars.Include(c => c.ReviewItems)
                .ThenInclude(ri => ri.Review)
                .Where(c => carIds.Contains(c.Id))

                //we use an anonymous type https://learn.microsoft.com/en-us/dotnet/csharp/fundamentals/types/anonymous-types
                .Select(c => new {
                    c.Id,
                    c.Model.Name,
                    c.FuelType,
                    c.Manufacturer,
                    c.Color,
                })
                .ToList();


            Review review = new Review(reviewForCreate.UserName,reviewForCreate.Country,
                reviewForCreate.Country, new List<RentalItem>());

            foreach (var item in reviewForCreate.ReviewItems)
            {
                var car = cars.FirstOrDefault(c => c.Id == item.CarId);
                //we must check that there is enough quantity to be rented in the database
                if (car == null)
                {
                    ModelState.AddModelError("ReviewItems", $"Error! Car with id '{item.CarId}' is not available for being reviewed");
                }
                else
                {
                    // review does not exist in the database yet and does not have a valid Id, so we must relate reviewitem to the object review
                    review.ReviewItems.Add(new ReviewItem(car.Id, review.Id, item.Description, item.Rating, car, review));
                }
            }

            //if there is any problem because of the available quantity of movies or because the movie does not exist
            if (ModelState.ErrorCount > 0)
            {
                return BadRequest(new ValidationProblemDetails(ModelState));
            }

            _context.Add(review);

            try
            {
                //we store in the database both rental and its rentalitems
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                ModelState.AddModelError("Review", $"Error! There was an error while saving your review, plese, try again later");
                return Conflict("Error" + ex.Message);

            }

            //it returns rentalDetail
            var reviewDetail = new ReviewDetailDTO(review.Id, review.Created,
                review.ApplicationUser.UserName, review.Country, review.DriverType,
                reviewForCreate.ReviewItems);

            return CreatedAtAction("GetReview", new { id = review.Id }, reviewDetail);
        }
    }
}
