using AppForSEII2526.API.DTOs.ReservaDTO;
using AppForSEII2526.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
                    .Include(r => r.Items)
                        .ThenInclude(ri => ri.Maintenance)
                .Select(r => new ReservaDetailDTO(r.Id, r.ApplicationUser.UserName, r.ClientAddress,       
                    (PaymentMethod)r.PaymentMethod,r.Date, r.Items.Select(ri => new ReservaItemDTO(ri.Maintenance.Id,
                    ri.Maintenance.Name, ri.Maintenance.Price, ri.Maintenance.NumberOfDays, ri.Comment)).ToList<ReservaItemDTO>())).FirstOrDefaultAsync();
            if (reserva == null)
            {
                _logger.LogError($"Error: La reserva con id {id} no existe.");
                return NotFound();
            }
            return Ok(reserva);
        }
    }
}