using System.ComponentModel;

namespace AppForSEII2526.API.Models
{
    public class Review
    {
        public Review()
        {
        }

        public Review(int id, string userName, string country, DateTime created, DriverTypes driverType)
        {
            Id = id;
            UserName = userName;
            Country = country;
            Created = created;
            DriverType = driverType;
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(20, ErrorMessage = "Name cannot be longer than 50 characters.")]
        public string UserName { get; set; }

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
