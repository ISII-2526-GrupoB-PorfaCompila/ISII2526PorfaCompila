namespace AppForSEII2526.API.Models
{
    public class Purchase
    {
        public Purchase()
        {
        }
        public Purchase(int purchaseId, string name, string surname, string deliveryCarDealer, DateTime purchasingDate, decimal purchasingPrice, PaymentMethod paymentMethod)
        {
            Id = purchaseId;
            Name = name;
            Surname = surname;
            DeliveryCarDealer = deliveryCarDealer;
            PurchasingDate = purchasingDate;
            PurchasingPrice = purchasingPrice;
            PaymentMethod = paymentMethod;
        }

        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }

        [DataType(System.ComponentModel.DataAnnotations.DataType.MultilineText)]
        [Display(Name = "Delivery Car Dealer")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please, set your Delivery car dealer.")]
        public string DeliveryCarDealer { get; set; }
        public DateTime PurchasingDate { get; set; }
        public decimal PurchasingPrice { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
    }
}
