using System.Drawing;

namespace AppForSEII2526.API.DTOs.PurchaseDTOs
{
    public class PurchaseDetailDTO
    {

        public PurchaseDetailDTO(int id, DateTime purchasingDate,string name, string surname, 
                                string deliveryCarDealer, decimal purchasePrice, IList<PurchaseItemDTO> purchaseItems)
        {
            Id = id;
            PurchasingDate = purchasingDate;
            Name = name;
            Surname = surname;
            DeliveryCarDealer = deliveryCarDealer;
            PurchasePrice = purchasePrice;
            PurchaseItems = purchaseItems;
        }
        public int Id { get; set; }
        public DateTime PurchasingDate { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string DeliveryCarDealer { get; set; }
        public decimal PurchasePrice { get; set; }
        public IList<PurchaseItemDTO> PurchaseItems { get; set; }
    }
}
