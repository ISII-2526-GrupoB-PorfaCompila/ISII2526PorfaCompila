using AppForSEII2526.API.DTOs.ReservaDTO;
using AppForSEII2526.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
namespace AppForSEII2526.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservasController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<ReservasController> _logger;

        public ReservasController(ApplicationDbContext context, ILogger<ReservasController> logger)
        {
            _context = context;
            _logger = logger;
        }
        [HttpGet]
        [Route("[action]")]
        [ProducesResponseType(typeof(ReservaDetailDTO), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult> GetReserva(int id)
        {
            if (_context.Bookings == null)
            {
                _logger.LogError("Error: La tabla reservas no existe.");
                return NotFound();
            }
            var reserva = await _context.Bookings
                .Where(r => r.Id == id)
                    .Include(r => r.ApplicationUser)
                    .Include(r => r.Items)
                        .ThenInclude(ri => ri.Maintenance)
                .Select(r => new ReservaDetailDTO(r.Id, r.ApplicationUser.Name, r.ClientAddress,       
                    (PaymentMethod)r.PaymentMethod,r.Date, r.Items.Select(ri => new ReservaItemDTO(ri.Maintenance.Id,
                    ri.Maintenance.Name, ri.Maintenance.Price, ri.Maintenance.NumberOfDays, ri.Comment)).ToList<ReservaItemDTO>())).FirstOrDefaultAsync();
            if (reserva == null)
            {
                _logger.LogError($"Error: La reserva con id {id} no existe.");
                return NotFound();
            }
            return Ok(reserva);
        }
        [HttpPost]
        [Route("[action]")]
        [ProducesResponseType(typeof(ReservaDetailDTO), (int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(ValidationProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.Conflict)]
        public async Task<ActionResult> CreateReserva(ReservaForCreateDTO reservaForCreate)
        {
            if (reservaForCreate.Date <= DateTime.Today)
                ModelState.AddModelError("Fecha inicio del mantenimiento", "Error! La fecha del mantenimiento no puede ser anterior a hoy.");
            if (reservaForCreate.ReservaItems.Count == 0)
                ModelState.AddModelError("ReservaItems", "Error! Necesitas seleccionar al menos 1 mantenimiento para la reserva.");
            var user = _context.ApplicationUsers.FirstOrDefault(au => au.UserName == reservaForCreate.ApplicationUser);
            if (user == null)
                ModelState.AddModelError("ReservaApplicationUser", "Error! El nombre de usuario no está registrado.");
            if (ModelState.ErrorCount > 0)
                return BadRequest(new ValidationProblemDetails(ModelState));

            var nombreMantenimiento = reservaForCreate.ReservaItems.Select(ri => ri.Name).ToList<string>();
            var mantenimientos = _context.Maintenances.Include(m => m.Bookings)
                .ThenInclude(ri => ri.Booking).Where(m => nombreMantenimiento.Contains(m.Name))
                .Select(m => new {
                    m.Id,
                    m.Name,
                    m.NumberOfDays,
                    m.Price,
                    NumberOfReservaItems = m.Bookings.Count(ri => ri.Booking.Date >= reservaForCreate.Date)
                })
                .ToList();

            Booking reserva = new Booking(user, reservaForCreate.ClientAddress, reservaForCreate.Date, (AppForSEII2526.API.Models.PaymentMethod)reservaForCreate.PaymentMethod, new List<BookingItem>());
            reserva.TotalPrice = 0;
            reserva.ApplicationUser = user;

            foreach (var item in reservaForCreate.ReservaItems)
            {
                var mantenimiento = mantenimientos.FirstOrDefault(m => m.Name == item.Name);
                if ((mantenimiento == null))
                {
                    ModelState.AddModelError("ReservaItems", $"Error! Mantenimiento con nombre '{item.Name}' no está disponible para realizar desde {reservaForCreate.Date.ToShortDateString()}");
                }
                else
                {
                    reserva.Items.Add(new BookingItem(reserva.Id, mantenimiento.Id, item.Comentarios));
                    item.Price = mantenimiento.Price;
                }
            }
            double total = 0;
            foreach (var ri in reserva.Items)
            {
                var m = mantenimientos.FirstOrDefault(mm => mm.Id == ri.MaintenanceId);
                if (m != null)
                {
                    total += (double)(m.Price * m.NumberOfDays);
                }
            }
            reserva.TotalPrice = total;

            if (ModelState.ErrorCount > 0)
            {
                return BadRequest(new ValidationProblemDetails(ModelState));
            }
            _context.Add(reserva);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                ModelState.AddModelError("Reserva", $"Error! Ha ocurrido un error al guardar tu reserva, intentelo de nuevo.");
                return Conflict("Error" + ex.Message);

            }
            var reservaDetail = new ReservaDetailDTO(reserva.Id, reservaForCreate.ApplicationUser, reserva.ClientAddress, reserva.PaymentMethod, reserva.Date, reservaForCreate.ReservaItems);

            return CreatedAtAction("GetReserva", new { id = reserva.Id }, reservaDetail);
        }
    }
}