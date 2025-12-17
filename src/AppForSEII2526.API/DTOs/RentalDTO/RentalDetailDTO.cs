
using Humanizer;

namespace AppForSEII2526.API.DTOs.RentalDTO
{
    public class RentalDetailDTO
    {
        public RentalDetailDTO(int id, string name, string surname, string address, 
            IList<RentalItemDTO> rentalItems, PaymentMethod paymentMethod, DateTime endDate, 
            DateTime rentingDate, DateTime startDate)
        {
            Id = id;
            Name = name;
            Surname = surname;
            DeliveryCarDealer = address;
            RentalItems = rentalItems;
            PaymentMethod = paymentMethod;
            EndDate = endDate;
            RentingDate = rentingDate;
            StartDate = startDate;
        }

        public RentalDetailDTO(string name, string surname, string address,
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
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        [Required]
        public string? Surname { get; set; }
        [Required]
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

        protected bool CompareDate(DateTime date1, DateTime date2)
        {
            return (date1.Subtract(date2) < new TimeSpan(0, 1, 0));
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

        public override bool Equals(object? obj)
        {
            return obj is RentalDetailDTO dTO &&
                   Name == dTO.Name &&
                   Surname == dTO.Surname &&
                   DeliveryCarDealer == dTO.DeliveryCarDealer &&
                   RentalItems.SequenceEqual(dTO.RentalItems) &&
                   PaymentMethod == dTO.PaymentMethod &&
                   CompareDate(EndDate, dTO.EndDate) &&
                   CompareDate(RentingDate, dTO.RentingDate) &&
                   CompareDate(StartDate, dTO.StartDate) &&
                   NumberOfDays == dTO.NumberOfDays &&
                   TotalPrice == dTO.TotalPrice;

        }

    }
}
