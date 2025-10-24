namespace AppForSEII2526.API.DTOs.ReviewDTO
{
    public class ReviewForCreateDTO
    {
        public ReviewForCreateDTO(string userName, string country, DriverTypes driverType, string? description, int rating, DateTime created, IList<ReviewItemDTO> reviewItems)
        {
            UserName = userName;
            Country = country;
            DriverType = driverType;
            Description = description;
            Rating = rating;
            Created = created;
            ReviewItems = reviewItems;
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

        [Required]
        [Display(Name = "Created Date")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Created { get; set; }

        public IList<ReviewItemDTO> ReviewItems { get; set; }
    }
}
