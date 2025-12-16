using AppForSEII2526.API.Controllers;
using AppForSEII2526.API.DTOs.PurchaseDTOs;
using AppForSEII2526.API.DTOs.RentalDTO;
using AppForSEII2526.API.Models;
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
            var models = new List<Model>()
            {
                new Model("FIAT"),
                new Model("KIA"),
                new Model("SEAT")
            };
            var cars = new List<Car>()
            {
                new Car(1, models[0], "Coche normal", "Rojo", "FIAT rojo gasolina","FIAT", new List<PurchaseItem>(), 15000, 5, "Gasolina"),
                new Car(2, models[1], "Coche eléctrico", "Blanco", "KIA blanco electrico","KIA", new List<PurchaseItem>(), 40000, 9, "Eléctrico"),
                new Car(3, models[2], "Coche híbrido", "Azul", "SEAT azul híbrido","SEAT", new List<PurchaseItem>(), 30000, 7, "Gasoil")
            };

            var user = new ApplicationUser("María", "Pérez");

            var purchasingDate = new DateTime(2024, 2, 20);

            var purchase = new Purchase(1,"Madrid", purchasingDate, user, new List<PurchaseItem>(), PaymentMethod.TarjetaDeCredito);

            purchase.PurchaseItems.Add(new PurchaseItem(purchase, cars[0], 1));
            purchase.PurchaseItems.Add(new PurchaseItem(purchase, cars[2], 2));

            purchase.PurchasingPrice = decimal.Round(purchase.PurchaseItems.Sum(pi => pi.Car.PurchasingPrice * pi.Quantity), 2);

            _context.AddRange(models);
            _context.AddRange(cars);
            _context.Add(user);
            _context.Add(purchase);
            _context.SaveChanges();
        }

        [Fact]
        [Trait("LevelTesting", "Unit Testing")]
        [Trait("Database", "WithoutFixture")]
        public async Task GetPurchase_NotFound_test()
        {
            // Arrange
            var mock = new Mock<ILogger<PurchaseController>>();
            ILogger<PurchaseController> logger = mock.Object;

            var controller = new PurchaseController(_context, logger);

            // Act
            var result = await controller.GetPurchase(0);

            //Assert
            //we check that the response type is OK and obtain the list of cars
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        [Trait("LevelTesting", "Unit Testing")]
        [Trait("Database", "WithoutFixture")]
        public async Task GetPurchase_Found_test()
        {
            // Arrange
            var mock = new Mock<ILogger<PurchaseController>>();
            ILogger<PurchaseController> logger = mock.Object;
            var controller = new PurchaseController(_context, logger);

            var expectedPurchase = new PurchaseDetailDTO(
                1,                 
                new DateTime(2024, 2, 20),    
                "María",          
                "Pérez",          
                "Madrid",         
                75000m,                
                new List<PurchaseItemDTO>()
            );

            expectedPurchase.PurchaseItems.Add(new PurchaseItemDTO(1, "FIAT", "Rojo", 1, 15000m));
            expectedPurchase.PurchaseItems.Add(new PurchaseItemDTO(3, "SEAT", "Azul", 2, 30000m));

            // Act 
            var result = await controller.GetPurchase(1);

            //Assert
            //we check that the response type is OK and obtain the rental
            var okResult = Assert.IsType<OkObjectResult>(result);
            var purchaseDTOActual = Assert.IsType<PurchaseDetailDTO>(okResult.Value);
            var eq = expectedPurchase.Equals(purchaseDTOActual);
            //we check that the expected and actual are the same
            Assert.Equal(expectedPurchase, purchaseDTOActual);

        }
    } 
}
