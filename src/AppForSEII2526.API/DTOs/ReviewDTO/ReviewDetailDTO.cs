using AppForSEII2526.API.Models;
using Microsoft.AspNetCore.Http.HttpResults;

namespace AppForSEII2526.API.DTOs.ReviewDTO
{
    public class ReviewDetailDTO : ReviewForCreateDTO
    {
        public ReviewDetailDTO(int id, DateTime created, string userName, string country, DriverTypes driverType, string? description, int rating, DateTime created, IList<ReviewItemDTO> reviewItems)
            : base (userName, country, driverType, description, rating, reviewItems)
        {
            Id = id;
            UserName = userName ?? throw new ArgumentNullException(nameof(userName));
            Country = country ?? throw new ArgumentNullException(nameof(country));
            DriverType = driverType;
            Description = description;
            Rating = rating;
            ReviewItems = reviewItems ?? throw new ArgumentNullException(nameof(reviewItems));
        }

        public int Id { get; set; }

        [Required]
        [Display(Name = "Created Date")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Created { get; set; }
    }
}
