namespace AppForSEII2526.API.Models
{
    [PrimaryKey(nameof(CarId), nameof(PurchaseId))]
    public class PurchaseItem
    {
        public PurchaseItem()
        {
        }
        public PurchaseItem(Purchase purchase, Car car, int quantity)
        {
            Purchase = purchase;
            PurchaseId = purchase.Id;
            Car = car;
            CarId = car.Id;
            Quantity = quantity;
        }
        public Purchase Purchase { get; set; }

        public int PurchaseId { get; set; }

        public Car Car { get; set; }

        public int CarId { get; set; }

        [Precision(10, 2)]
        public decimal Price { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "You must provide a quantity higher than 1")]
        public int Quantity { get; set; }
    }
}
