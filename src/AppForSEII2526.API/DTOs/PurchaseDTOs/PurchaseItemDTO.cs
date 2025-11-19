using System.Drawing;

namespace AppForSEII2526.API.DTOs.PurchaseDTOs
{
    public class PurchaseItemDTO
    {

        public PurchaseItemDTO() { }

        // Constructor del Detalle de Compra
        public PurchaseItemDTO(int carId, string model, string color, int quantityForPurchase ,decimal priceForPurchase)
        {
            CarId = carId;
            Model = model;
            Color = color;
            PriceForPurchase = priceForPurchase;
            QuantityForPurchase = quantityForPurchase;
        }

        // Constructor de Crear Compra
        public PurchaseItemDTO(int carId, string model, string color,decimal priceForPurchase, int quantityForPurchase, string description)
        {
            CarId = carId;
            Model = model;
            Color = color;
            PriceForPurchase = priceForPurchase;
            QuantityForPurchase = quantityForPurchase;
            Description = description;
        }

        public int CarId { get; set; }
        public string Model { get; set; }
        public string Color { get; set; }
        public decimal PriceForPurchase { get; set; }

        [Required]
        public int QuantityForPurchase { get; set; }
        public string Description { get; set; }

        public override bool Equals(object? obj)
        {
            return obj is PurchaseItemDTO dTO &&
                   CarId == dTO.CarId &&
                   Model == dTO.Model &&
                   Color == dTO.Color &&
                   PriceForPurchase == dTO.PriceForPurchase &&
                   QuantityForPurchase == dTO.QuantityForPurchase &&
                   Description == dTO.Description;
        }
    }
}
