namespace AppForSEII2526.API.Controllers
{
    public class TipoMantenimientoController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private ILogger _logger;

        public TipoMantenimientoController(ApplicationDbContext context, ILogger<CarsController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet]
        [Route("[action]")]
        [ProducesResponseType(typeof(IList<string>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> GetTipoMantenimiento(string? nombreTipo)
        {

            IList<string> tipos = (IList<string>)await _context.MaintenanceTypes
                .Where(tipo => (nombreTipo == null || tipo.Type.Contains(nombreTipo))) // where clause             
                .OrderBy(tipo => tipo.Type)
                .Select(tipo => tipo.Type)
                .ToListAsync();

            return Ok(tipos);
        }
    }
}
