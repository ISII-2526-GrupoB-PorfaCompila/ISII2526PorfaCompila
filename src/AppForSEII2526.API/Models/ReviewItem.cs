namespace AppForSEII2526.API.Models
{
    [PrimaryKey(nameof(CarId), nameof(ReviewId))]
    public class ReviewItem
    {
        public ReviewItem()
        {
        }

        /*public ReviewItem(int carId, int rating, Review review)
        {
            CarId = carId;
            Rating = rating;
            Review = review;
        }

        public ReviewItem(int carId, int rating, Review review, string? description) : this(carId, rating, review) => Description = description;
        */

        public ReviewItem(Review review, Car car, int rating, string? description)
        {
            Car = car;
            CarId = car.Id;
            Review = review;
            ReviewId = review.Id;
            Rating = rating;
            Description = description;
        }

        [Required]
        public Car Car { get; set; }

        public int CarId { get; set; }

        [Required]
        public Review Review { get; set; }

        public int ReviewId { get; set; }

        [Required]
        [Range(1, 5, ErrorMessage = "You must provide a rating between 1 and 5.")]
        public int Rating { get; set; }

        [StringLength(50, ErrorMessage = "Description cannot be longer than 50 characters.")]
        public string? Description { get; set; }
    }
}
