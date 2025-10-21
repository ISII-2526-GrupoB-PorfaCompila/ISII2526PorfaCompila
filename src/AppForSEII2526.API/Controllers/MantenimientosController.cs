using AppForSEII2526.API.DTOs.MantenimientosDTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AppForSEII2526.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MantenimientosController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<MantenimientosController> _logger;

        public MantenimientosController(ApplicationDbContext context, ILogger<MantenimientosController> logger)
        {
            _context = context;
            _logger = logger;
        }
        [HttpGet]
        [Route("[action]")]
        [ProducesResponseType(typeof(IList<MantenimientoDTO>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetMantenimientos(string? nombre, string? tipo)
        {
            var mantenimientos = await _context.MaintenanceTypes.Include(Mantenimientos => Mantenimientos.Maintenance)
                .Where(c => (c.Maintenance.Name.Contains(nombre) || (nombre == null)) && ((c.Type.Contains(tipo)) || (tipo == null)))
                .Select(c => new MantenimientoDTO(c.Maintenance.Id,c.Maintenance.Name,c.Type,c.Maintenance.Price,c.Maintenance.NumberOfDays))
                .ToListAsync();
            return Ok(mantenimientos);
        }

    }
}
