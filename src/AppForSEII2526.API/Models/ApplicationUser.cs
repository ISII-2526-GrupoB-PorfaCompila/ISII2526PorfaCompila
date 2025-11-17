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

    //Constructor CU2 // Alquilar coches
    public ApplicationUser(int id, string name, string surname, IList<Rental> rentals)
    {
        Id = id;
        Name = name;
        Surname = surname;
        Rentals = rentals;
    }
    //Constructor para el POST
    public ApplicationUser(string name, string surname)
    {
        Name = name;
        Surname = surname;
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
    public ApplicationUser(int id, string username, IList<Review> reviews)
    {
        Id = id;
        UserName = username;
        Reviews = reviews;
    }
    //Contructor para el POST
    public ApplicationUser(string username)
    {
        UserName = username;
    }

    [Key]
    public int Id { get; set; }

    [Display(Name = "Name")]
    public string? Name { get; set; }

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