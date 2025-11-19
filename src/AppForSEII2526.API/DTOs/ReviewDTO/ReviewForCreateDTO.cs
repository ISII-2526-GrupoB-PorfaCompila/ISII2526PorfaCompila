
namespace AppForSEII2526.API.DTOs.ReviewDTO
{
    public class ReviewForCreateDTO
    {
        public ReviewForCreateDTO(string userName, string country, DriverTypes driverType, IList<ReviewItemDTO> reviewItems)
        {
            UserName = userName ?? throw new ArgumentNullException(nameof(userName));
            Country = country ?? throw new ArgumentNullException(nameof(country));
            DriverType = driverType;
            ReviewItems = reviewItems ?? throw new ArgumentNullException(nameof(reviewItems));
        }

        [Required]
        public string UserName { get; set; }

        [Required]
        public string Country {  get; set; }

        [Required]
        public DriverTypes DriverType { get; set; }

        public IList<ReviewItemDTO> ReviewItems { get; set; }

        public override bool Equals(object? obj)
        {
            return obj is ReviewForCreateDTO dTO &&
                   UserName == dTO.UserName &&
                   Country == dTO.Country &&
                   DriverType == dTO.DriverType &&
                   ReviewItems.SequenceEqual(dTO.ReviewItems);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(UserName, Country, DriverType, ReviewItems);
        }
    }
}
