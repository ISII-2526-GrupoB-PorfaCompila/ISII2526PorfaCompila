using Microsoft.AspNetCore.Identity;

namespace AppForSEII2526.API.Models;

// Add profile data for application users by adding properties to the ApplicationUser class
public class ApplicationUser : IdentityUser {
    public ApplicationUser() 
    { 
    }
    //Constructor CU1
    public ApplicationUser(int id, string name, string surname, IList<Purchase> purchases)
    {
        Id = id;
        Name = name;
        Surname = surname;
        Purchases = purchases;
    }

    [Key]
    public int Id { get; set; }

    [Required]
    [Display(Name = "Name")]
    public string Name { get; set; }

    [Display(Name = "Surname")]
    public string? Surname { get; set; }

    [Required]
    public IList<Purchase> Purchases { get; set; }
}