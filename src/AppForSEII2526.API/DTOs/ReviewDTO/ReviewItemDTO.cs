
namespace AppForSEII2526.API.DTOs.ReviewDTO
{
    public class ReviewItemDTO
    {
        public ReviewItemDTO(int carId, string model, string manufacturer, string color, int rating, string description = "")
        {
            CarId = carId;
            Model = model;
            Manufacturer = manufacturer;
            Color = color;
            Rating = rating;
            Description = description;
        }

        public int CarId { get; set; }

        public string Model { get; set; }

        public string Manufacturer { get; set; }

        public string Color { get; set; }

        public int Rating { get; set; }

        public string? Description { get; set; }

        public override bool Equals(object? obj)
        {
            return obj is ReviewItemDTO dTO &&
                   CarId == dTO.CarId &&
                   Model == dTO.Model &&
                   Manufacturer == dTO.Manufacturer &&
                   Color == dTO.Color &&
                   Rating == dTO.Rating &&
                   Description == dTO.Description;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(CarId, Model, Manufacturer, Color, Rating, Description);
        }
    }
}
