using System.Drawing;

namespace AppForSEII2526.API.DTOs.PurchaseDTOs
{
    public class PurchaseItemDTO
    {
        public PurchaseItemDTO(int carId, string model, decimal priceForPurchase)
        {
            CarId = carId;
            Model = model;
            PriceForPurchase = priceForPurchase;
        }

        public int CarId { get; set; }
        public string Model { get; set; }
        public decimal PriceForPurchase { get; set; }
    }
}
