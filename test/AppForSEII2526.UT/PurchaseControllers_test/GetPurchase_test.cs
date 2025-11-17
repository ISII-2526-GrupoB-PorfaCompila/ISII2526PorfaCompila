using AppForSEII2526.API.Controllers;
using AppForSEII2526.API.DTOs.PurchaseDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppForSEII2526.UT.PurchaseControllers_test
{
    public class GetPurchase_test : AppForSEII25264SqliteUT
    {
        public GetPurchase_test()
        {
            // --- Datos de prueba ---

            // Modelo
            var model = new Model(1, "Model S", new List<Car>());

            // Coche (CU1 - comprar coches)
            var purchaseItemsForCar = new List<PurchaseItem>() { };
            var car = new Car(
                id: 1,
                model: model,
                carClass: "Sedan",
                color: "Red",
                description: "Electric car",
                manufacturer: "Tesla",
                purchaseItems: purchaseItemsForCar,
                purchasingPrice: 30000m,
                quantityForPurchasing: 5,
                new EngDispacement = "N/A"
            );

            // Usuario
            var purchasesForUser = new List<Purchase>();
            var user = new ApplicationUser(
                id: 1,
                name: "Juan",
                surname: "Pérez",
                purchases: purchasesForUser
            );

            // Compra
            var purchasingDate = new DateTime(2024, 1, 1);
            var purchase = new Purchase()
            {
                Id = 1,
                DeliveryCarDealer = "Concesionario Centro",
                PurchasingDate = purchasingDate,
                ApplicationUser = user,
                PurchaseItems = new List<PurchaseItem>(),
                PaymentMethod = PaymentMethod.TarjetaDeCredito,
                PurchasingPrice = 30000m
            };

            // Item de compra
            var purchaseItem = new PurchaseItem(purchase, car, quantity: 1);

            // Relacionamos todo
            purchase.PurchaseItems.Add(purchaseItem);
            purchaseItemsForCar.Add(purchaseItem);
            purchasesForUser.Add(purchase);

            // Persistimos en la BD en memoria
            _context.Models.Add(model);
            _context.Cars.Add(car);
            _context.ApplicationUsers.Add(user);
            _context.Purchases.Add(purchase);
            _context.Add(purchaseItem); 
            _context.SaveChanges();
        }

        [Fact]
        [Trait("Database", "WithoutFixture")]
        [Trait("LevelTesting", "Unit Testing")]
        public async Task GetPurchase_NotFound_test()
        {
            // Arrange
            var mock = new Mock<ILogger<CarsController>>();
            ILogger<CarsController> logger = mock.Object;

            var controller = new PurchaseController(_context, logger);

            // Act
            var result = await controller.GetPurchase(0); // id que no existe

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        [Trait("LevelTesting", "Unit Testing")]
        [Trait("Database", "WithoutFixture")]
        public async Task GetPurchase_Found_test()
        {
            // Arrange
            var mock = new Mock<ILogger<CarsController>>();
            ILogger<CarsController> logger = mock.Object;
            var controller = new PurchaseController(_context, logger);

            var purchasingDate = new DateTime(2024, 1, 1);
           
            var expectedPurchase = new PurchaseDetailDTO(
                id: 1,
                purchasingDate: purchasingDate,
                name: "Juan",
                surname: "Pérez",
                deliveryCarDealer: "Concesionario Centro",
                purchasePrice: 30000m,
                purchaseItems: new List<PurchaseItemDTO>()
            );

            // ¡Cuidado! El controlador usa:
            // QuantityForPurchase = pi.Car.QuantityForPurchasing
            // PriceForPurchase    = pi.Car.PurchasingPrice
            expectedPurchase.PurchaseItems.Add(
                new PurchaseItemDTO(
                    carId: 1,
                    model: "Model S",
                    color: "Red",
                    quantityForPurchase: 5,   // QuantityForPurchasing del coche
                    priceForPurchase: 30000m  // PurchasingPrice del coche
                )
            );

            // Act
            var result = await controller.GetPurchase(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var purchaseDTOActual = Assert.IsType<PurchaseDetailDTO>(okResult.Value);

            // Comprobamos que el DTO devuelto es el esperado (usa Equals sobreescrito)
            Assert.Equal(expectedPurchase, purchaseDTOActual);
        }
    }
}
