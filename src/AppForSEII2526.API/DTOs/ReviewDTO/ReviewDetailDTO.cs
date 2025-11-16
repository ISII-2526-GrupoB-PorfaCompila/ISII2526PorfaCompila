using AppForSEII2526.API.Models;
using Microsoft.AspNetCore.Http.HttpResults;

namespace AppForSEII2526.API.DTOs.ReviewDTO
{
    public class ReviewDetailDTO : ReviewForCreateDTO
    {
        public ReviewDetailDTO(int id, DateTime created, string userName, string country, DriverTypes driverType, IList<ReviewItemDTO> reviewItems)
            : base (userName, country, driverType, reviewItems)
        {
            Id = id;
            Created = created;
        }

        public int Id { get; set; }

        [Required]
        [Display(Name = "Created Date")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Created { get; set; }

        public override bool Equals(object? obj)
        {
            return obj is ReviewDetailDTO dTO &&
                   UserName == dTO.UserName &&
                   Country == dTO.Country &&
                   DriverType == dTO.DriverType &&
                   ReviewItems.SequenceEqual(dTO.ReviewItems) &&
                   Id == dTO.Id &&
                   Created == dTO.Created;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(UserName, Country, DriverType, ReviewItems, Id, Created);
        }
    }
}
