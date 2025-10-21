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

    //Constructor CU2
    public ApplicationUser(int id, string name, string surname, IList<Rental> rentals)
    {
        Id = id;
        Name = name;
        Surname = surname;
        Rentals = rentals;
    }

    //Constructor CU3
    public ApplicationUser(int id, string name, string surname, IList<Booking> bookings, string? clientPhoneNumber)
    {
        Id = id;
        Name = name;
        Surname = surname;
        Bookings = bookings;
        ClientPhoneNumber = clientPhoneNumber;
    }

    //Constructor CU4
    public ApplicationUser(int id, string name, string surname, IList<Review> reviews)
    {
        Id = id;
        Name = name;
        Surname = surname;
        Reviews = reviews;
    }

    [Key]
    public int Id { get; set; }

    [Required]
    [Display(Name = "Name")]
    public string Name { get; set; }

    [Display(Name = "Surname")]
    public string? Surname { get; set; }
    public string? ClientPhoneNumber { get; set; }

    [Required]
    public IList<Purchase> Purchases { get; set; }

    [Required]
    public IList<Rental> Rentals { get; set; }

    [Required]
    public IList<Booking> Bookings { get; set; }

    [Required]
    public IList<Review> Reviews { get; set; }
}