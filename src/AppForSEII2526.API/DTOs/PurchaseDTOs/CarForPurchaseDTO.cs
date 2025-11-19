using System.Drawing;

namespace AppForSEII2526.API.DTOs.PurchaseDTOs
{
    public class CarForPurchaseDTO
    {
        public CarForPurchaseDTO(int id, string model, string color, string manufacturer, string fuelType,decimal purchasingPrice)
        {
            Id = id;
            Model = model;
            Color = color;
            Manufacturer = manufacturer;
            FuelType = fuelType;
            PurchasingPrice = purchasingPrice;
        }

        public int Id { get; set; }
        public string Model { get; set; }
        public string Color { get; set; }
        public string Manufacturer { get; set; }
        public decimal PurchasingPrice { get; set; }
        public string FuelType { get; set; }

        public override bool Equals(object? obj)
        {
            return obj is CarForPurchaseDTO dTO &&
                   Id == dTO.Id &&
                   Model == dTO.Model &&
                   Color == dTO.Color &&
                   Manufacturer == dTO.Manufacturer &&
                   PurchasingPrice == dTO.PurchasingPrice &&
                   FuelType == dTO.FuelType;
        }
    }
}
