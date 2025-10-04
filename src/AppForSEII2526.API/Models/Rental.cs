using Microsoft.EntityFrameworkCore;

namespace AppForSEII2526.API.Models
{
    public class Rental
    {
        public Rental()
        {
        }

        public Rental(int id, double totalPrice, string deliveryCarDealer, string name, string surname, DateTime endDate, DateTime rentingDate, DateTime startDate, PaymentMethod paymentMethod)
        {
            Id = id;
            TotalPrice = totalPrice;
            DeliveryCarDealer = deliveryCarDealer;
            Name = name;
            Surname = surname;
            EndDate = endDate;
            RentingDate = rentingDate;
            StartDate = startDate;
            PaymentMethod = paymentMethod;
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

        [StringLength(20, ErrorMessage = "Name cannot be longer than 20 characters.")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please, set your name")]
        public string Name { get; set; } //creo que esto y surname hay que sustituirlo por ApplicationUser

        [StringLength(25, ErrorMessage = "Title cannot be longer than 25 characters.")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please, set your Surname")]
        public string Surname { get; set; }

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
    }
}
