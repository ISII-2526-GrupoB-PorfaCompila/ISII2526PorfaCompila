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

        [Required]
        public Purchase Purchase { get; set; }

        [Required]
        public int PurchaseId { get; set; }

        [Required]
        public Car Car { get; set; }

        [Required]
        public int CarId { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "You must provide a quantity higher than 1")]
        public int Quantity { get; set; }
    }
}
