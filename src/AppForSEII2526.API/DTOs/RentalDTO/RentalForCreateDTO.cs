namespace AppForSEII2526.API.DTOs.RentalDTO
{
    public class RentalForCreateDTO
    {

        /* ------ Atributos de aquí 
          
          5. El sistema muestra los coches seleccionados, indicando su modelo, fabricante y 
          precio de alquiler, y solicita al usuario su nombre, apellidos, dirección y método de pago 
          (Visa, Google Pay o Paypal) y la cantidad de cada coche seleccionado, siendo todos datos 
          obligatorios. 
          
         ------- */

        public RentalForCreateDTO()
        {
        }

        public RentalForCreateDTO(string name, string surname, string deliveryCarDealer, IList<RentalItemDTO> rentalItems, 
            PaymentMethod paymentMethod, DateTime endDate, DateTime startDate)
        {
            Name = name;
            Surname = surname;
            DeliveryCarDealer = deliveryCarDealer;
            RentalItems = rentalItems;
            PaymentMethod = paymentMethod;
            EndDate = endDate;
            StartDate = startDate;
        }

        [Required]
        public string Name { get; set; }
        [Required]
        public string Surname { get; set; }
        [Required]
        public string? DeliveryCarDealer { get; set; }

        public IList<RentalItemDTO> RentalItems { get; set; }

        [Required]
        public PaymentMethod PaymentMethod { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime EndDate { get; set; }

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
