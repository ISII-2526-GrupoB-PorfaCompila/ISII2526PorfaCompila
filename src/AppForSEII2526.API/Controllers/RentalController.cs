using AppForSEII2526.API.DTOs.RentalDTO;
using Microsoft.AspNetCore.Mvc;

namespace AppForSEII2526.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RentalController : Controller
    {

        private readonly ApplicationDbContext _context; // Atributo para acceder a la Base de Datos
        private readonly ILogger<RentalController> _logger; // Atributo para registrar mensajes

        //inicialización de los atributos
        public RentalController(ApplicationDbContext context, ILogger<RentalController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet]
        [Route("[action]")]
        [ProducesResponseType(typeof(RentalDetailDTO), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult> GetRentalDetail(int id)
        {
            if (_context.Rentals == null)
            {
                _logger.LogError("Error: Rentals table does not exist");
                return NotFound();
            }

            var rental = await _context.Rentals
             .Where(r => r.Id == id)
                 .Include(r => r.ApplicationUser) //añade los datos del ApplicationUser
                 .Include(r => r.RentalItems)     //añade los RentalItemDTOs
                    .ThenInclude(r => r.Car)      //añade los datos del Car de cada RentalItem
             .Select(r => new RentalDetailDTO(
                 r.Id,
                 r.ApplicationUser.Name, 
                 r.ApplicationUser.Surname, 
                 r.DeliveryCarDealer, 
                 r.RentalItems
                    .Select (ri => 
                                new RentalItemDTO(
                                    ri.Car.Id,
                                    ri.Car.Manufacturer,
                                    ri.Quantity,
                                    ri.Car.RentingPrice,
                                    ri.Car.Model.Name)
                                )
                    .ToList(),
                 r.PaymentMethod,
                 r.EndDate,
                 r.RentingDate,
                 r.StartDate))
             .FirstOrDefaultAsync();

            if (rental == null)
            {
                _logger.LogError($"Error: Rental with id {id} does not exist");
                return NotFound();
            }

            return Ok(rental);
        }

        [HttpPost]
        [Route("[action]")]
        [ProducesResponseType(typeof(RentalDetailDTO), (int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(ValidationProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.Conflict)]
        public async Task<ActionResult> CreateRental(RentalForCreateDTO rentalForCreate)
        {

            //cada validacion definida en RentalForCreate se comprueba antes de ejecutar el método, por lo que no es necesario comprobarlas de nuevo
            if (rentalForCreate.StartDate <= DateTime.Today)
                ModelState.AddModelError("RentalDateFrom", "Error! Your rental date must start later than today");

            if (rentalForCreate.StartDate >= rentalForCreate.EndDate)
                ModelState.AddModelError("RentalDateFrom&RentalDateTo", "Error! Your rental must end later than it starts");

            if (rentalForCreate.RentalItems.Count == 0)
                ModelState.AddModelError("RentalItems", "Error! You must include at least one car to be rented");

            //Comprobación del examen.
            if (!rentalForCreate.DeliveryCarDealer.Contains("Calle")) 
                ModelState.AddModelError("RentalCalle", "¡Error! La dirección de envío tiene que empezar por la palabra Calle");

            var user = _context.ApplicationUsers.FirstOrDefault(au => au.Name == rentalForCreate.Name);
            if (user == null)
                ModelState.AddModelError("RentalApplicationUser", "Error! That user is not registered");

            if (ModelState.ErrorCount > 0)
                return BadRequest(new ValidationProblemDetails(ModelState));

            //esto creo que solo vale para probar si esos coches están en stock, pero no sale en mi caso de uso...
            //var carNames = rentalForCreate.RentalItems.Select(ri => ri.Id).ToList();

            //var cars = _context.Cars.Include(c => c.RentalItems)
            //    .ThenInclude(ri => ri.Rental)
            //    .Where(c => carNames.Contains(c.Id)).ToList()

            //    .Select(c => new
            //    {
            //        c.Id,
            //        c.QuantityForRenting,
            //        c.RentingPrice,
            //    })
            //    .ToList();

            Rental rental = new Rental(new List<RentalItem>(), rentalForCreate.TotalPrice, rentalForCreate.DeliveryCarDealer, 
                new ApplicationUser(rentalForCreate.Name, rentalForCreate.Surname), rentalForCreate.EndDate, rentalForCreate.StartDate, rentalForCreate.PaymentMethod);

            foreach (var item in rentalForCreate.RentalItems)
            {
                var car = _context.Cars.FirstOrDefault(c => c.Id == item.Id);
                rental.RentalItems.Add(new RentalItem(car, rental, rental.Id, car.Id, item.QuantityForRenting));
                item.RentingPrice = car.RentingPrice;
            }

            _context.Add(rental);

            try
            {
                //we store in the database both rental and its rentalitems
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                ModelState.AddModelError("Rental", $"Error! There was an error while saving your rental, plese, try again later");
                return Conflict("Error" + ex.Message);

            }

            var rentalDetail = new RentalDetailDTO(
                 rental.Id,
                 rentalForCreate.Name,
                 rentalForCreate.Surname,
                 rental.DeliveryCarDealer,
                 rentalForCreate.RentalItems,
                 rentalForCreate.PaymentMethod,
                 rentalForCreate.EndDate,
                 rental.RentingDate,
                 rentalForCreate.StartDate);

            return CreatedAtAction("GetRentalDetail", new { id = rental.Id }, rentalDetail);
        }
    }
}
