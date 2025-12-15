using AppForSEII2526.API.Models;
using AppForSEII2526.Web.API;

namespace AppForSEII2526.Web
{
    public class PurchaseStateContainer
    {
        public PurchaseForCreateDTO Purchase { get; private set; } = new PurchaseForCreateDTO() { 
            PurchaseItems = new List<PurchaseItemDTO>()
            };

        public decimal TotalPrice
        {
            get
            {
                return Convert.ToDecimal(Purchase.PurchaseItems.Sum(pi => pi.PriceForPurchase * pi.QuantityForPurchase));
            }
        }
        public event Action? OnChange;

        private void NotifyStateChanged() => OnChange?.Invoke();

        public void AddPurchaseItem(CarForPurchaseDTO car)
        {
            if (!Purchase.PurchaseItems.Any(pi => pi.CarId == car.Id)) 
            {
                Purchase.PurchaseItems.Add(new PurchaseItemDTO()
                {
                    CarId = car.Id,
                    Model = car.Model,
                    Color = car.Color,
                    PriceForPurchase = car.PurchasingPrice,
                    QuantityForPurchase = 1,
                });
            }
        }

        public void RemovePurchaseItem(PurchaseItemDTO item)
        {
            Purchase.PurchaseItems.Remove(item);
        }

        public void ClearPurchasingCart()
        {
            Purchase.PurchaseItems.Clear();
        }

        public void PurchaseProcessed()
        {
            Purchase = new PurchaseForCreateDTO()
            {
                PurchaseItems = new List<PurchaseItemDTO>()
            };
        }

    }
}
