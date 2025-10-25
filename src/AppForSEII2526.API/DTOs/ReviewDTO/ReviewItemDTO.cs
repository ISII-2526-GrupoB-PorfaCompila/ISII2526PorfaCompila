namespace AppForSEII2526.API.DTOs.ReviewDTO
{
    public class ReviewItemDTO
    {
        public ReviewItemDTO(int carId, string manufacturer, string fuelType, string? description, int rating)
        {
            CarId = carId;
            Manufacturer = manufacturer;
            FuelType = fuelType;
            Description = description;
            Rating = rating;
        }

        public int CarId { get; set; }

        public string Manufacturer { get; set; }

        public string FuelType { get; set; }

        public string? Description { get; set; }

        public int Rating { get; set; }

        
    }
}
