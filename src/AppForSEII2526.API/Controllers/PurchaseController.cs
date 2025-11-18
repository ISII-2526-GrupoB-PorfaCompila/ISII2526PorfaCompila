using AppForSEII2526.API.DTOs.PurchaseDTOs;
using AppForSEII2526.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AppForSEII2526.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PurchaseController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<PurchaseController> _logger;
        public PurchaseController(ApplicationDbContext context, ILogger<PurchaseController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet]
        [Route("[action]")]
        [ProducesResponseType(typeof(PurchaseItemDTO), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetPurchase(int id)
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
                        pi.Car.Color,
                        pi.Car.QuantityForPurchasing,
                        pi.Car.PurchasingPrice)).ToList()
                )).FirstOrDefaultAsync();

            if (purchase == null)
            {
                return NotFound();
            }

            return Ok(purchase);
        }


        [HttpPost]
        [Route("[action]")]
        [ProducesResponseType(typeof(PurchaseDetailDTO), (int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(ValidationProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.Conflict)]
        public async Task<IActionResult> CreatePurchase([FromBody] PurchaseForCreateDTO purchaseForCreate)
        {
            if (purchaseForCreate.PurchaseItems.Count <= 0)
            {
                ModelState.AddModelError("PurchaseItems", "Error! You must include at least one car to be purchased");
            }

            if (ModelState.ErrorCount > 0)
                return BadRequest(new ValidationProblemDetails(ModelState));

            var user = await _context.Users.FirstOrDefaultAsync(au => au.Name == purchaseForCreate.Name && au.Surname == purchaseForCreate.Surname);
            if (user == null)
                ModelState.AddModelError("PurchaseApplicationUser", "Error! UserName is not registered");

            var carsIds = purchaseForCreate.PurchaseItems.Select(pi => pi.CarId).ToList();

            var cars = await _context.Cars.Include(c => c.PurchaseItems).ThenInclude(pi => pi.Purchase)
                .Where(c => carsIds.Contains(c.Id))
                .Select(c => new
                {
                    c.Id,
                    c.Model.Name,
                    c.Color,
                    c.Description,
                    c.PurchasingPrice,
                }).ToListAsync();

            Purchase purchase = new Purchase( purchaseForCreate.DeliveryCarDealer, DateTime.Now, user,
                        new List<PurchaseItem>(), purchaseForCreate.PaymentMethod);

            purchase.PurchasingPrice = 0;
            foreach (var item in purchaseForCreate.PurchaseItems)
            {
                var car = cars.FirstOrDefault(c => c.Id == item.CarId);
                if (car == null)
                {
                    ModelState.AddModelError("CarId", $"Error! The car with Id {item.CarId} does not exist.");
                }
                purchase.PurchaseItems.Add(new PurchaseItem(purchase, car.Id, item.QuantityForPurchase));
                purchase.PurchasingPrice += car.PurchasingPrice * item.QuantityForPurchase;
            }

            if (ModelState.ErrorCount > 0)
            {
                return BadRequest(new ValidationProblemDetails(ModelState));
            }

            _context.Add(purchase);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                ModelState.AddModelError("Purchase", $"Error! There was an error while saving your purchase, plese, try again later");
                return Conflict("Error" + ex.Message);

            }

            var purchaseDetail = new PurchaseDetailDTO
            (
                purchase.Id,
                purchase.PurchasingDate,
                purchaseForCreate.Name,
                purchaseForCreate.Surname,
                purchaseForCreate.DeliveryCarDealer,
                purchase.PurchasingPrice,
                purchaseForCreate.PurchaseItems
            );
            return CreatedAtAction("GetPurchase", new { id = purchase.Id }, purchaseDetail);

        }
    }
}
