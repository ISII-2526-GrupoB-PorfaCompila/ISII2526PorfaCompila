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
    [StringLength(20,ErrorMessage ="El nombre tiene que tener entre 1 y 20 caracteres.", MinimumLength =1)]
    public string ClientName { get; set; }
    [StringLength(20, ErrorMessage = "El apellido tiene que tener entre 1 y 20 caracteres.", MinimumLength = 1)]
    public string ClientSurname { get; set; }
    [StringLength(30, ErrorMessage = "La direccion tiene que tener entre 1 y 30 caracteres.", MinimumLength = 1)]
    public string ClientAddress { get; set; }
    public string? ClientPhoneNumber { get; set; }
    public PaymentMethod PaymentMethod { get; set; }

    public Booking()
    {
    }
    public Booking(int id, string clientName, string clientSurname, string clientAddress, string clientPhoneNumber, PaymentMethod paymentMethod)
    {
        Id = id;
        ClientName = clientName;
        ClientSurname = clientSurname;
        ClientAddress = clientAddress;
        ClientPhoneNumber = clientPhoneNumber;
        PaymentMethod = paymentMethod;
    }
        
    }
}