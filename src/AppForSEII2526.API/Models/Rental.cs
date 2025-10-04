namespace AppForSEII2526.API.Models
{
    public class Rental
    {
        public int Id { get; set; }
        public double TotalPrice { get; set; }
        public string DeliveryCarDealer { get; set; }
        public string Name { get; set; } //creo que esto y surname hay que sustituirlo por ApplicationUser
        public string Surname { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime RentingDate { get; set; }
        public DateTime StartDate { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
    }
}
