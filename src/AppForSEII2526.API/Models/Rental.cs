using Microsoft.EntityFrameworkCore;

namespace AppForSEII2526.API.Models
{
    public class Rental
    {
        public Rental()
        {
        }

        public Rental(int id, IList<RentalItem> rentalItems, double totalPrice, string deliveryCarDealer, ApplicationUser applicationUser , DateTime endDate, DateTime rentingDate, DateTime startDate, PaymentMethod paymentMethod)
        {
            Id = id;
            TotalPrice = totalPrice;
            DeliveryCarDealer = deliveryCarDealer;
            ApplicationUser =applicationUser;
            EndDate = endDate;
            RentingDate = rentingDate;
            StartDate = startDate;
            PaymentMethod = paymentMethod;
            RentalItems = rentalItems;
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [Precision(10, 2)]
        public double TotalPrice { get; set; }

        [DataType(System.ComponentModel.DataAnnotations.DataType.MultilineText)]
        [StringLength(50, ErrorMessage = "Cannot be longer than 50 characters.")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please, set your delivery car dealer")]
        public string DeliveryCarDealer { get; set; }

        [Required]
        ApplicationUser ApplicationUser { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime EndDate { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime RentingDate { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime StartDate { get; set; }

        [Required]
        public PaymentMethod PaymentMethod { get; set; }

        //relación con RentalItem
        [Required]
        public IList<RentalItem> RentalItems { get; set; }
    }
}
