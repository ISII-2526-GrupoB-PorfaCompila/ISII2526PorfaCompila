namespace AppForSEII2526.API.DTOs.RentalDTO
{
    /*Cada instancia de RentalItemDTO representa un coche. Ergo la IList de RentalDetailDTO es una
     lista de coches (con los atributos que nos interesan de cada uno.*/

    public class RentalItemDTO
    {

        public RentalItemDTO(int id, string manufacturer, int quantityForRenting, 
            double rentingPrice, string model)
        {
            Id = id;
            Manufacturer = manufacturer;
            QuantityForRenting = quantityForRenting;
            RentingPrice = rentingPrice;
            Model = model;
        }

        [Required]
        public int Id { get; set; }

        [Required]
        public string Manufacturer { get; set; }

        [Required]
        [Display(Name = "Quantity For Renting")]
        [Range(1, int.MaxValue, ErrorMessage = "Minimum quantity for renting is 1")]
        public int QuantityForRenting { get; set; }

        [Required]
        [Precision(10, 2)]
        public double RentingPrice { get; set; }

        //es string porque es más fácil de manejar en el front
        [Required]
        public string Model { get; set; }

    }
}
