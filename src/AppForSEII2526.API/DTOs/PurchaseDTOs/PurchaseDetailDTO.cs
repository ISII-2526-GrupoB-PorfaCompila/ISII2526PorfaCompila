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

        public override bool Equals(object? obj)
        {
            return obj is PurchaseDetailDTO dTO &&
                   Id == dTO.Id &&
                   PurchasingDate == dTO.PurchasingDate &&
                   Name == dTO.Name &&
                   Surname == dTO.Surname &&
                   DeliveryCarDealer == dTO.DeliveryCarDealer &&
                   PurchasePrice == dTO.PurchasePrice &&
                   PurchaseItems.SequenceEqual(dTO.PurchaseItems);
                   //EqualityComparer<IList<PurchaseItemDTO>>.Default.Equals(PurchaseItems, dTO.PurchaseItems);
        }
    }
}
