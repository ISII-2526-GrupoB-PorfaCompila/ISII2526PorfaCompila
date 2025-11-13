using AppForSEII2526.API.DTOs.RentalDTO;
using Microsoft.AspNetCore.Mvc;

namespace AppForSEII2526.API.Controllers
{
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
                 r.ApplicationUser.Name, 
                 r.ApplicationUser.Surname, 
                 r.DeliveryCarDealer, 
                 r.RentalItems
                    .Select (ri => 
                                new RentalItemDTO(
                                    ri.Car.Id,
                                    ri.Car.Manufacturer,
                                    ri.Car.QuantityForRenting,
                                    ri.Car.RentingPrice,
                                    ri.Car.Model.ToString())
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
    }
}
