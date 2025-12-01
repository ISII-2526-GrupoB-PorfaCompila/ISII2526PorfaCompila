using AppForSEII2526.API.Models;
using AppForSEII2526.Web.API;

namespace AppForSEII2526.Web
{
    public class RentalStateContainer
    {

        //creamos una instancia de Rental cuando creamos una instancia de RentalStateContainer
        public RentalForCreateDTO Rental { get; private set; } = new RentalForCreateDTO()
        {
            RentalItems = new List<RentalItemDTO>()
        };

        //sacamos el TotalPrice de los coches que hemos seleccionado para alquilar
        public decimal TotalPrice
        {
            get
            {
                int numberOfDays = (Rental.EndDate - Rental.StartDate).Days;
                return Convert.ToDecimal(Rental.RentalItems.Sum(ri => ri.RentingPrice * numberOfDays));
            }
        }

        public event Action? OnChange;

        private void NotifyStateChanged() => OnChange?.Invoke();



        public void AddCarToRental(CarForRentalDTO car)
        {

            if (!Rental.RentalItems.Any(ri => ri.Id == car.Id)) //Antes de añadir un car comprobamos si ya ha sido añadido

                Rental.RentalItems.Add(new RentalItemDTO()
                {
                    Id = car.Id,
                    Model = car.Model,
                    Manufacturer = car.Manufacturer
                }
            );

        }

        public void RemoveRentalItemToRent(RentalItemDTO item)
        {
            Rental.RentalItems.Remove(item);

        }

        public void ClearRentingCart()
        {
            Rental.RentalItems.Clear();

        }

        //Una vez terminado el proceso creamos el Rental.
        public void RentalProcessed()
        {
            //hemos terminado el proceso asiq creamos un nuevo objeto sin datos
            Rental = new RentalForCreateDTO()
            {
                RentalItems = new List<RentalItemDTO>()
            };
        }
    }
}