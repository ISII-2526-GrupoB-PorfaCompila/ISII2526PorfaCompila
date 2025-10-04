namespace AppForSEII2526.API.Models
{
public class MaintenanceType
{
    [Key]
    public int id { get; set; }

    [StringLength(20,ErrorMessage = "El tipo debe tener entre 1 y 20 caracteres.", MinimumLength = 1)]    
    public string Type { get; set; }
    public Maintenance Maintenance { get; set; }

    public MaintenanceType()
    {
        
    }
    public MaintenanceType(int id, string type, Maintenance maintenance)
    {
        this.id = id;
        Type = type;
        Maintenance = maintenance;
    }
}
}
