namespace AppForSEII2526.API.DTOs.AlquilerDTO
{
    public class CarForRentalDTO
    {

        /*2. El sistema muestra el conjunto de coches disponibles para alquilar desde hoy hasta la 
        próxima semana, indicando el nombre del modelo, el tipo de gasoil, el fabricante, el 
        precio del alquiler y el color. */

        public CarForRentalDTO(int id, string model, string color, string manufacturer, string fuelType, double rentingPrice)
        {
            Id = id;
            Model = model;
            Color = color;
            Manufacturer = manufacturer;
            FuelType = fuelType;
            RentingPrice = rentingPrice;
        }

        public int Id { get; set; }
        public string Model { get; set; }
        public string Color { get; set; }
        public string Manufacturer { get; set; }
        public string FuelType { get; set; }
        public double RentingPrice { get; set; }
    }
}
