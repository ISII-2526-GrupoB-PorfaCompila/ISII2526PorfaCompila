namespace AppForSEII2526.API.Models {

    /* Dudas
            1. Cambiar el nombre de paymentmethod a paymenmethodtypes?
            2. Debe de estar esta clase aquí?
     */
    public enum PaymentMethod 
{
    TarjetaDeCredito,
    Paypal,
    Metalico,
    Visa,
    GooglePay,
    PayPal
}

public class Booking
{
        [Key]
        public int Id { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Es obligatorio introducir tu nombre de usuario.")]
        public ApplicationUser ApplicationUser { get; set; }
        [Required(AllowEmptyStrings = true, ErrorMessage = "Por favor, introduzca una dirección.")]
        public string ClientAddress { get; set; }
        [Required]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }
        [Required(ErrorMessage = "Por favor, seleccione un método de pago.")]
        public PaymentMethod PaymentMethod { get; set; }
        public IList<BookingItem> Items { get; set; }

        public Booking()
        {
        }
        public Booking(int id, ApplicationUser applicationUser, string clientAddress, PaymentMethod paymentMethod, IList<BookingItem> items)
        {
            Id = id;
            ApplicationUser = applicationUser;
            ClientAddress = clientAddress;
            PaymentMethod = paymentMethod;
            Items = items;
        }

    }
}