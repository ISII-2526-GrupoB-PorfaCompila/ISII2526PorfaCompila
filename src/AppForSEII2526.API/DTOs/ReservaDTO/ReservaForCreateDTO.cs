namespace AppForSEII2526.API.DTOs.ReservaDTO
{
    public class ReservaForCreateDTO
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Por favor, introduzca un nombre de usuario.")]
        public string ApplicationUser { get; set; }
        [Required(AllowEmptyStrings = true, ErrorMessage = "Por favor, introduzca una dirección.")]
        public string ClientAddress { get; set; }
        [Required]
        public PaymentMethod PaymentMethod { get; set; }
        [Required]
        public DateTime Date { get; set; }
        public IList<ReservaItemDTO> ReservaItems { get; set; }
        [Display(Name = "Total Price")]
        [JsonPropertyName("TotalPrice")]
        public double TotalPrice
        {
            get
            {
                return (double)ReservaItems.Sum(ri => ri.Price * ri.NumberOfDays);
            }
        }
        public ReservaForCreateDTO(string applicationUser, string clientAddress, PaymentMethod paymentMethod, DateTime date, IList<ReservaItemDTO> reservaItems)
        {
            if (applicationUser is null) throw new ArgumentNullException(nameof(applicationUser));
            if (clientAddress is null) throw new ArgumentNullException(nameof(clientAddress));
            if (reservaItems is null) throw new ArgumentNullException(nameof(reservaItems));

            ApplicationUser = applicationUser;
            ClientAddress = clientAddress;
            PaymentMethod = paymentMethod;
            Date = date;
            ReservaItems = reservaItems;
        }
        public ReservaForCreateDTO(IList<ReservaItemDTO> reservaItems)
        {
            ReservaItems = reservaItems;
        }
        public ReservaForCreateDTO()
        {

        }

        public override bool Equals(object? obj)
        {
            return obj is ReservaForCreateDTO dTO &&
                   ApplicationUser == dTO.ApplicationUser &&
                   ClientAddress == dTO.ClientAddress &&
                   PaymentMethod == dTO.PaymentMethod &&
                   Date == dTO.Date &&
                   ReservaItems.SequenceEqual(dTO.ReservaItems) &&
                   TotalPrice == dTO.TotalPrice;
        }
        public override int GetHashCode()
        {
            return HashCode.Combine(ApplicationUser, ClientAddress, PaymentMethod, Date, ReservaItems, TotalPrice);
        }
    }
}
