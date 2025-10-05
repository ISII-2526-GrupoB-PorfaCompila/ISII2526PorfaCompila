namespace AppForSEII2526.API.Models
{
public class BookingItem
{
    public int BookingId { get; set; }
    public int MantId { get; set; }
    [StringLength(50, ErrorMessage = "El comentario es obligatorio. Debe tener como maximo 50 caracteres.", MinimumLength = 1)]
    public string Comment { get; set; }
    public Booking Booking { get; set; }
    public Maintenance Maintenance { get; set; }
    public BookingItem()
    {
        
    }
    public BookingItem(int bookingId, int mantId, string comment, Booking booking, Maintenance maintenance)
    {
        BookingId = bookingId;
        MantId = mantId;
        Comment = comment;
        Booking = booking;
        Maintenance = maintenance;
    }
    }
}
