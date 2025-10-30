namespace AppForSEII2526.API.DTOs.ReviewDTO
{
    public class ReviewItemDTO
    {
        public ReviewItemDTO(int carId, Model model, string manufacturer, string color, int rating, string description = "")
        {
            CarId = carId;
            Model = model;
            Manufacturer = manufacturer;
            Color = color;
            Rating = rating;
            Description = description;
        }

        public int CarId { get; set; }

        public Model Model { get; set; }

        public string Manufacturer { get; set; }

        public string Color { get; set; }

        public int Rating { get; set; }

        public string? Description { get; set; }
    }
}
