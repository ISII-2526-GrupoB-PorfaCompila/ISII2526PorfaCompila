namespace AppForSEII2526.API.DTOs.ReviewDTO
{
    public class ReviewForCreateDTO
    {
        public ReviewForCreateDTO(string userName, string country, DriverTypes driverType, string? description, int rating, IList<ReviewItemDTO> reviewItems)
        {
            UserName = userName ?? throw new ArgumentNullException(nameof(userName));
            Country = country ?? throw new ArgumentNullException(nameof(country));
            DriverType = driverType;
            Description = description;
            Rating = rating;
            ReviewItems = reviewItems ?? throw new ArgumentNullException(nameof(reviewItems));
        }

        [Required]
        public string UserName { get; set; }

        [Required]
        public string Country {  get; set; }

        [Required]
        public DriverTypes DriverType { get; set; }

        public string? Description { get; set; }

        [Required]
        public int Rating { get; set; }

        public IList<ReviewItemDTO> ReviewItems { get; set; }
    }
}
