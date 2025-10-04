namespace AppForSEII2526.API.Models
{
public class Maintenance
{
    [Key]
    public int Id { get; set; }
    
    [StringLength(20,ErrorMessage = "El nombre debe tener entre 1 y 20 caracteres.", MinimumLength = 1)]
    public string Name { get; set; }
    
    [Range(1,50,ErrorMessage = "El numero de dias debe ser entre 1 y 50.")]
    public int NumberOfDays { get; set; }
    
    [DataType(System.ComponentModel.DataAnnotations.DataType.Currency)]
    [Range(1, 500, ErrorMessage = "El precio debe estar entre 1 y 500 euros.")]
    [Display(Name = "Price")]
    public decimal Price { get; set; }
    
    public Maintenance(){
        
    }
    public Maintenance(int id, string name, int numberOfDays, int price)
    {
        Id = id;
        Name = name;
        NumberOfDays = numberOfDays;
        Price = price;
    }
    }
}
