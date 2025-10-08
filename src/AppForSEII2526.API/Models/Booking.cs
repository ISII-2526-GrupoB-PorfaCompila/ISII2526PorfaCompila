namespace AppForSEII2526.API.Models {
public enum PaymentMethod
{
    TarjetaDeCredito,
    Paypal,
    Metalico
}

public class Booking
{
    [Key]
    public int Id { get; set; }
    [Required(AllowEmptyStrings = false, ErrorMessage = "Es obligatorio introducir tu nombre de usuario.")]
    public ApplicationUser ApplicationUser { get; set; }
    [Required(AllowEmptyStrings = false, ErrorMessage = "Por favor, introduzca una dirección.")]
    public string ClientAddress { get; set; }
    public string? ClientPhoneNumber { get; set; }
    [Required(ErrorMessage = "Por favor, seleccione un método de pago.")]
    public PaymentMethod PaymentMethod { get; set; }

    public Booking()
    {
    }
    public Booking(int id, ApplicationUser applicationUser, string clientAddress, string clientPhoneNumber, PaymentMethod paymentMethod)
    {
        Id = id;
        ApplicationUser = applicationUser;
        ClientAddress = clientAddress;
        ClientPhoneNumber = clientPhoneNumber;
        PaymentMethod = paymentMethod;
    }
        
    }
}