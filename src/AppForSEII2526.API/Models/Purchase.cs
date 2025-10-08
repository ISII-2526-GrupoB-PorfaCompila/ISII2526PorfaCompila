namespace AppForSEII2526.API.Models
{
    public class Purchase
    {
        public Purchase()
        {
            PurchaseItems = new List<PurchaseItem>();
        }

        //public Purchase(int purchaseId, string name, string surname, string deliveryCarDealer, DateTime purchasingDate, ApplicationUser applicationUser, IList<PurchaseItem> purchaseItems, PaymentMethod paymentMethod)
        //    : this(0,name, surname, deliveryCarDealer, purchasingDate, 0, applicationUser, purchaseItems, paymentMethod)
        //{            
        //}

        public Purchase(int purchaseId, string deliveryCarDealer, DateTime purchasingDate, decimal purchasingPrice, ApplicationUser applicationUser,IList<PurchaseItem> purchaseItems, PaymentMethod paymentMethod)
        {
            PurchasingPrice = decimal.Round(purchaseItems.Sum(pi => pi.Price * pi.Quantity),2);

            Id = purchaseId;
            DeliveryCarDealer = deliveryCarDealer;
            PurchasingDate = purchasingDate;
            ApplicationUser = applicationUser;
            PurchaseItems = purchaseItems;
            PaymentMethod = paymentMethod;
        }

        [Key]
        public int Id { get; set; }

        [DataType(System.ComponentModel.DataAnnotations.DataType.MultilineText)]
        [Display(Name = "Delivery Car Dealer")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please, set your Delivery car dealer.")]
        public string DeliveryCarDealer { get; set; }

        public DateTime PurchasingDate { get; set; }

        [Precision(10, 2)]
        public decimal PurchasingPrice { get; set; }

        public ApplicationUser ApplicationUser { get; set; }

        public IList<PurchaseItem> PurchaseItems { get; set; }

        [Display(Name = "Payment Method")]
        public PaymentMethod PaymentMethod { get; set; }
    }
}
