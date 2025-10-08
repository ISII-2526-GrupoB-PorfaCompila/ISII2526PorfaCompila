using System.ComponentModel;

namespace AppForSEII2526.API.Models
{
    public class Review
    {
        public Review()
        {
        }

        public Review(int id, ApplicationUser applicationUser, string country, DateTime created, DriverTypes driverType)
        {
            Id = id;
            ApplicationUser = applicationUser;
            Country = country;
            Created = created;
            DriverType = driverType;
        }

        [Key]
        public int Id { get; set; }

        [Required]
        public ApplicationUser ApplicationUser { get; set; }

        [Required]
        public string Country { get; set; }

        [Required]
        [Display(Name = "Created Date")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Created {  get; set; }

        [Required]
        public DriverTypes DriverType { get; set; }
    }

    public enum DriverTypes
    {
        Novato,
        Experto
    }
}
