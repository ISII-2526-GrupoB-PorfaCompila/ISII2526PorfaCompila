namespace AppForSEII2526.API.Models
{

    [PrimaryKey(nameof(CarId), nameof(RentalId))]
    public class RentalItem
    {
        public RentalItem(Car car, Rental rental, int rentalId, int carId, int quantity)
        {
            Car = car;
            Rental = rental;
            RentalId = rentalId;
            CarId = carId;
            Quantity = quantity;
        }

        // relaciones
        public Car Car { get; set; }
        public Rental Rental { get; set; }

        [Required]
        public int RentalId { get; set; }
        
        [Required]
        public int CarId { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "You must provide a quantity higher than 1")]
        public int Quantity { get; set; }

    }
}
