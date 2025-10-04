using System.ComponentModel;

namespace AppForSEII2526.API.Models
{
    public class Car
    {
        public Car()
        {
        }

        //Constructor Para CU4-Reseñar coches
        public Car(int id, /*Model model,*/ string carClass, string color, string? description, string engDispacement, string fuelType, /*MaintenanceType maintenanceTypes,*/ string manufacturer, /*PurchaseItem purchaseItems,*/ double purchasingPrice, int quantityForPurchasing, int quantityForRenting, /*RentalItems rentalItems,*/ double rentingPrice, int rimSize/*, IList<ReviewItem> reviewItems*/)
        {
            Id = id;
            //Model = model;
            CarClass = carClass;
            Color = color;
            Description = description;
            EngDispacement = engDispacement;
            FuelType = fuelType;
            //MaintenanceTypes = maintenanceTypes;
            Manufacturer = manufacturer;
            //PurchaseItems = purchaseItems;
            PurchasingPrice = purchasingPrice;
            QuantityForPurchasing = quantityForPurchasing;
            QuantityForRenting = quantityForRenting;
            //RentalItems = rentalItems;
            RentingPrice = rentingPrice;
            RimSize = rimSize;
            //ReviewItems = reviewItems;
        }

        [Key]
        public int Id { get; set; }

        //public Model Model { get; set; }

        public string CarClass {  get; set; }

        public string Color { get; set; }

        [StringLength(100, ErrorMessage = "Description cannot be longer than 100 characters.")]
        public string? Description { get; set; }

        public string EngDispacement { get; set; }

        public string FuelType { get; set; }

        //public MaintenanceType MaintenanceTypes { get; set; }

        public string Manufacturer { get; set; }

        //public PurchaseItem PurchaseItems { get; set; }

        [Precision(10, 2)]
        public double PurchasingPrice { get; set; }

        [Display(Name = "Quantity For Purchasing")]
        [Range(1,int.MaxValue, ErrorMessage = "Minimum quantity for purchasing is 1")]
        public int QuantityForPurchasing { get; set; }

        [Display(Name = "Quantity For Renting")]
        [Range(1, int.MaxValue, ErrorMessage = "Minimum quantity for renting is 1")]
        public int QuantityForRenting { get; set; }

        //public RentalItems RentalItems { get; set; }

        [Precision(10, 2)]
        public double RentingPrice { get; set; }

        public int RimSize { get; set; }

        //public IList<ReviewItem> ReviewItems {  get; set; } 
    }
}
