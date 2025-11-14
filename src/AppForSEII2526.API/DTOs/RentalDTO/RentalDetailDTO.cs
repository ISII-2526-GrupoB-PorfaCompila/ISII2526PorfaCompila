namespace AppForSEII2526.API.DTOs.RentalDTO
{
    public class RentalDetailDTO
    {
        public RentalDetailDTO(string name, string? surname, string? address, 
            IList<RentalItemDTO> rentalItems, PaymentMethod paymentMethod, DateTime endDate, 
            DateTime rentingDate, DateTime startDate)
        {
            Name = name;
            Surname = surname;
            DeliveryCarDealer = address;
            RentalItems = rentalItems;
            PaymentMethod = paymentMethod;
            EndDate = endDate;
            RentingDate = rentingDate;
            StartDate = startDate;
        }

        [Required]
        public string Name { get; set; }
        public string? Surname { get; set; }
        public string? DeliveryCarDealer { get; set; } //esto es el address

        public IList<RentalItemDTO> RentalItems { get; set; }

        [Required]
        public PaymentMethod PaymentMethod { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime EndDate { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime RentingDate { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime StartDate { get; set; }

        public int NumberOfDays
        {
            get
            {
                return (EndDate - StartDate).Days;
            }
        }

        [Display(Name = "Total Price")]
        [JsonPropertyName("TotalPrice")]
        public double TotalPrice
        {
            get
            {
                return RentalItems.Sum(ri => ri.RentingPrice * NumberOfDays);
            }
        }

    }
}
