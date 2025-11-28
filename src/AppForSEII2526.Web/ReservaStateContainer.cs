using AppForSEII2526.Web.API;

namespace AppForSEII2526.Web
{
    public class ReservaStateContainer
    {
        public ReservaForCreateDTO Reserva { get; private set; } = new ReservaForCreateDTO()
        {
            ReservaItems = new List<ReservaItemDTO>()
        };
        public decimal TotalPrice
        {
            get
            {
                return (decimal)Reserva.ReservaItems.Sum(ri => ri.Price * ri.NumberOfDays);
            }
        }
        public event Action? OnChange;
        private void NotifyStateChanged() => OnChange?.Invoke();
        public void AddMantenimiento(MantenimientoDTO mantenimiento)
        {
            if (!Reserva.ReservaItems.Any(ri => ri.ReservaId == mantenimiento.Id))
                //we add it if it is not in the list
                Reserva.ReservaItems.Add(new ReservaItemDTO()
                {
                   ReservaId = mantenimiento.Id,
                   Name = mantenimiento.Nombre,
                   Price = mantenimiento.Precio,
                   NumberOfDays = mantenimiento.NumeroDias,
                }
            );
        }
        public void RemoveMantenimiento(ReservaItemDTO item)
        {
            Reserva.ReservaItems.Remove(item);
        }
        public void ClearReservaCart()
        {
            Reserva.ReservaItems.Clear();
        }
        public void ReservaProcessed()
        {
            Reserva = new ReservaForCreateDTO()
            {
                ReservaItems = new List<ReservaItemDTO>()
            };
        }
    }
}
