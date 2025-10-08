using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using AppForSEII2526.API.Models;

namespace AppForSEII2526.API.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser>(options)
{
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<PurchaseItem>().HasKey(pi => new { pi.PurchaseId, pi.CarId });
        builder.Entity<RentalItem>().HasKey(pi => new { pi.RentalId, pi.CarId });
        builder.Entity<BookingItem>().HasKey(pi => new { pi.BookingId, pi.MantId });
        builder.Entity<ReviewItem>().HasKey(pi => new { pi.ReviewId, pi.CarId });
    }

    public DbSet<Purchase> Purchases { get; set; }
    public DbSet<Rental> Rentals { get; set; }
    public DbSet<Booking> Bookings { get; set; }
    public DbSet<Maintenance> Maintenances { get; set; }
    public DbSet<MaintenanceType> MaintenanceTypes { get; set; }
    public DbSet<Review> Reviews { get; set; }
    public DbSet<Model> Models { get; set; }
    public DbSet<Car> Cars { get; set; }
    public DbSet<ApplicationUser> ApplicationUsers { get; set; }

}
