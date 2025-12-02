namespace AppForSEII2526.API.DTOs.PurchaseDTOs
{
    public class PurchaseForCreateDTO
    {
        public PurchaseForCreateDTO()
        {
            PurchaseItems = new List<PurchaseItemDTO>();
        }
        public PurchaseForCreateDTO(string name, string surname, string deliveryCarDealer, 
                                    PaymentMethod paymentMethod, IList<PurchaseItemDTO> purchaseItems)
        {
            Name = name;
            Surname = surname;
            DeliveryCarDealer = deliveryCarDealer;
            PaymentMethod = paymentMethod;
            PurchaseItems = purchaseItems;
        }

        [Display(Name = "Nombre del Cliente")]
        [Required]
        public string Name { get; set; }

        [Display(Name = "Apellido del Cliente")]
        [Required]
        public string Surname { get; set; }

        [Required]
        public string DeliveryCarDealer { get; set; }

        [Required]
        public PaymentMethod PaymentMethod { get; set; }

        public IList<PurchaseItemDTO> PurchaseItems { get; set; }
    }
}
