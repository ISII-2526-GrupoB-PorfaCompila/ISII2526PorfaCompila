using AppForSEII2526.API.DTOs.ReviewDTO;
using AppForSEII2526.API.Models;

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
                    .Include(r => r.ApplicationUser)
                    .Include(r => r.ReviewItems) //join table ReviewItems
                        .ThenInclude(ri => ri.Car) //then join table Cars
                .Select(r => new ReviewDetailDTO(r.Id, r.Created,r.ApplicationUser.UserName,
                    r.Country, (DriverTypes)r.DriverType,
                    r.ReviewItems
                        .Select(ri => new ReviewItemDTO(ri.Car.Id, ri.Car.Model.Name,
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

            //cada validacion definida en ReviewForCreate se comprueba antes de ejecutar el método, por lo que no es necesario comprobarlas de nuevo
            if (reviewForCreate.ReviewItems.Count == 0)
                ModelState.AddModelError("ReviewItems", "Error! You must include at least one car to be reviewed");

            var user = _context.ApplicationUsers.FirstOrDefault(au => au.UserName == reviewForCreate.UserName);
            if (user == null)
                ModelState.AddModelError("ReviewApplicationUser", "Error! That user is not registered");

            if (ModelState.ErrorCount > 0)
                return BadRequest(new ValidationProblemDetails(ModelState));

            Review review = new Review(user, reviewForCreate.Country, DateTime.Now,
                reviewForCreate.DriverType, new List<ReviewItem>());

            foreach (var item in reviewForCreate.ReviewItems)
            {
                var car = _context.Cars.FirstOrDefault(c => c.Id == item.CarId);
                review.ReviewItems.Add(new ReviewItem(review, car, item.Rating, item.Description));
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

            var reviewDetail = new ReviewDetailDTO(
                review.Id,
                review.Created,
                reviewForCreate.UserName,
                reviewForCreate.Country,
                reviewForCreate.DriverType,
                reviewForCreate.ReviewItems);

            return CreatedAtAction("GetReview", new { id = review.Id }, reviewDetail);
        }
    }
}