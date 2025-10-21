namespace AppForSEII2526.API.DTOs.MantenimientosDTO
{
    public class MantenimientoDTO
    {
        public int Id { get; set; }
        [Required]
        public string Nombre { get; set; }
        [Required]
        public string Tipo { get; set; }
        [Required]
        [DataType(System.ComponentModel.DataAnnotations.DataType.Currency)]
        [Range(1, 500, ErrorMessage = "El precio debe estar entre 1 y 500 euros.")]
        [Display(Name = "Precio")]
        public decimal Precio { get; set; }
        [Required]
        public int NumeroDias { get; set; }
        public MantenimientoDTO(int id, string nombre, string tipo, decimal precio, int numeroDias)
        {
            Id = id;
            Nombre = nombre;
            Tipo = tipo;
            Precio = precio;
            NumeroDias = numeroDias;
        }
    }
}
