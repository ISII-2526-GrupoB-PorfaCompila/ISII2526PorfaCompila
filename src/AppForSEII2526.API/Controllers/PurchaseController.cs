using AppForSEII2526.API.DTOs.PurchaseDTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AppForSEII2526.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PurchaseController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<CarsController> _logger;
        public PurchaseController(ApplicationDbContext context, ILogger<CarsController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet]
        [Route("[action]")]
        [ProducesResponseType(typeof(PurchaseItemDTO), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetBuy(int id)
        {
            if (_context.Purchases == null)
            {
                _logger.LogError("Purchases DbSet is null.");
                return NotFound();
            }
            var purchase = await _context.Purchases.Where(p => p.Id == id)
                .Include(p => p.ApplicationUser)
                .Include(p => p.PurchaseItems)
                .ThenInclude(pi => pi.Car)
                .ThenInclude(c => c.Model)
                .Select(p => new PurchaseDetailDTO(
                    p.Id,
                    p.PurchasingDate,
                    p.ApplicationUser.Name,
                    p.ApplicationUser.Surname,
                    p.DeliveryCarDealer,
                    p.PurchasingPrice,
                    p.PurchaseItems.Select(pi => new PurchaseItemDTO(
                        pi.Car.Id,
                        pi.Car.Model.Name,
                        pi.Car.PurchasingPrice)).ToList()
                )).FirstOrDefaultAsync();

            return Ok(purchase);
        }
    }
}
