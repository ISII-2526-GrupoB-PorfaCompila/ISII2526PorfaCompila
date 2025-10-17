namespace AppForSEII2526.API.Models
{
    [PrimaryKey(nameof(BookingId), nameof(MaintenanceId))]
    public class BookingItem
    {
        public int BookingId { get; set; }
        public int MaintenanceId { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "El comentario es obligatorio. Introduzca un comentario.")]
        public string Comment { get; set; }
        public Booking Booking { get; set; }
        public Maintenance Maintenance { get; set; }
        public BookingItem()
        {

        }
        public BookingItem(int bookingId, int maintenanceId, string comment, Booking booking, Maintenance maintenance)
        {
            BookingId = bookingId;
            MaintenanceId = maintenanceId;
            Comment = comment;
            Booking = booking;
            Maintenance = maintenance;
        }
    }
}