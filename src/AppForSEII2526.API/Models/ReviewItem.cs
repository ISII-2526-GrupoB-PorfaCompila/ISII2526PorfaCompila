namespace AppForSEII2526.API.Models
{
    public class ReviewItem
    {
        public ReviewItem()
        {
        }

        public ReviewItem(int carId, int reviewId, string? description, int rating, Car car, Review review)
        {
            CarId = carId;
            ReviewId = reviewId;
            Description = description;
            Rating = rating;
            Car = car;
            Review = review;
        }

        public int CarId { get; set; }

        public int ReviewId { get; set; }

        [StringLength(50, ErrorMessage = "Description cannot be longer than 50 characters.")]
        public string? Description { get; set; }

        [Required]
        [Range(1,5, ErrorMessage = "You must provide a rating between 1 and 5.")]
        public int Rating { get; set; }

        public Car Car { get; set; }

        public Review Review { get; set; }
    }
}
