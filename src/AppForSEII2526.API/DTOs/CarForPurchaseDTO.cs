using System.Drawing;

namespace AppForSEII2526.API.DTOs
{
    public class CarForPurchaseDTO
    {
        public CarForPurchaseDTO(int id, string model, string color, string manufacturer, decimal purchasingPrice)
        {
            Id = id;
            Model = model;
            Color = color;
            Manufacturer = manufacturer;
            PurchasingPrice = purchasingPrice;
        }

        public int Id { get; set; }
        public string Model { get; set; }
        public string Color { get; set; }
        public string Manufacturer { get; set; }
        public decimal PurchasingPrice { get; set; }
    }
}
