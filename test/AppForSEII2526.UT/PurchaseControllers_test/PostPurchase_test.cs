using AppForSEII2526.API.Controllers;
using AppForSEII2526.API.DTOs.PurchaseDTOs;
using AppForSEII2526.API.DTOs.RentalDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppForSEII2526.UT.PurchaseControllers_test
{
    public class PostPurchase_test : AppForSEII25264SqliteUT
    {
        private const string _name = "María";
        private const string _surname = "Pérez";
        private const string _deliveryCarDealer = "Concesionario Central";
        private const PaymentMethod _paymentMethod = PaymentMethod.Visa;

        private const string _model1 = "FIAT";
        private const string _model2 = "KIA";
        private const string _color1 = "Rojo";
        private const string _color2 = "Blanco";
        private const string _carClass1 = "Compacto";
        private const string _carClass2 = "Deportivo";
        private const string _manufacturer = "FabricaCoches";

        public PostPurchase_test()
        {
            var models = new List<Model>()
            {
                new Model(_model1),
                new Model(_model2)
            };

            var cars = new List<Car>(){
                new Car(1, models[0], _carClass1,_color1,"FIAT rojo?", _manufacturer, new List<PurchaseItem>(), 15000, 9, "Gasolina" ),
                new Car(2, models[1], _carClass2,_color2, "KIA blanco?", _manufacturer, new List<PurchaseItem>(), 20000, 8, "Eléctrico" ),
                new Car(3, models[1], _carClass2,_color2, ".", _manufacturer, new List<PurchaseItem>(), 20000, 8, "Eléctrico" )
            };

            var user = new ApplicationUser(_name, _surname);

            var purchase = new Purchase(_deliveryCarDealer, DateTime.Today, user, new List<PurchaseItem>(), _paymentMethod);
            _context.AddRange(models);
            _context.AddRange(cars);
            _context.Add(purchase);
            _context.SaveChanges();
        }
        public static IEnumerable<object[]> TestCasesFor_CreatePurchase()
        {
            // 1) Sin coches en la compra
            var purchaseNoItem = new PurchaseForCreateDTO(
                _name,
                _surname,
                _deliveryCarDealer,
                _paymentMethod,
                new List<PurchaseItemDTO>() // lista vacía
            );

            // 2) Usuario NO registrado (nombre distinto)
            var purchaseUserNotRegistered = new PurchaseForCreateDTO(
                "Fulgencio",            
                _surname,
                _deliveryCarDealer,
                _paymentMethod,
                new List<PurchaseItemDTO>(){
                    new PurchaseItemDTO
                    {
                        CarId = 1,
                        QuantityForPurchase = 1
                    }
                });
            // 3) Examen POST
            var purchaseNoDescripcion = new PurchaseForCreateDTO(
                "Fulgencio",
                _surname,
                _deliveryCarDealer,
                _paymentMethod,
                new List<PurchaseItemDTO>(){
                    new PurchaseItemDTO
                    {
                        CarId = 3,
                        QuantityForPurchase = 2
                    }
                });
            var allTests = new List<object[]>
            {
                new object[] { purchaseNoItem, "Error! You must include at least one car to be purchased" },
                new object[] { purchaseUserNotRegistered, "Error! UserName is not registered" },
                new object[] { purchaseNoDescripcion, "¡Error! Estás comprando demasiados coches sin descripción" }
            };
            return allTests;
        }

        [Theory]
        [Trait("LevelTesting", "Unit Testing")]
        [Trait("Database", "WithoutFixture")]
        [MemberData(nameof(TestCasesFor_CreatePurchase))]
        public async Task CreatePurchase_Error_test(PurchaseForCreateDTO purchaseDTO, string errorExpected)
        {
            // Arrange
            var mock = new Mock<ILogger<PurchaseController>>();
            ILogger<PurchaseController> logger = mock.Object;

            var controller = new PurchaseController(_context, logger);

            // Act
            var result = await controller.CreatePurchase(purchaseDTO);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            var problemDetails = Assert.IsType<ValidationProblemDetails>(badRequestResult.Value);

            var errorActual = problemDetails.Errors.First().Value[0];

            Assert.StartsWith(errorExpected, errorActual);
        }
        [Fact]
        [Trait("LevelTesting", "Unit Testing")]
        [Trait("Database", "WithoutFixture")]
        public async Task CreatePurchase_Success_test()
        {
            // Arrange
            var mock = new Mock<ILogger<PurchaseController>>();
            ILogger<PurchaseController> logger = mock.Object;

            var controller = new PurchaseController(_context, logger);

            // Queremos comprar 1 FIAT (id 1) y 1 KIA (id 2)
            var purchaseItems = new List<PurchaseItemDTO>()
            {
                new PurchaseItemDTO
                {
                    CarId = 1,
                    QuantityForPurchase = 1
                },
                new PurchaseItemDTO
                {
                    CarId = 2,
                    QuantityForPurchase = 1
                }
            };

            var purchaseDTO = new PurchaseForCreateDTO(
                _name,
                _surname,
                _deliveryCarDealer,
                _paymentMethod,
                purchaseItems
            );
            // Precio esperado: 15000 + 20000 = 35000
            decimal expectedTotalPrice = 15000m + 20000m;

            var expectedpurchaseDetailDTO = new PurchaseDetailDTO
            (
                2,
                DateTime.Today,
                _name,
                _surname,
                _deliveryCarDealer,
                expectedTotalPrice, 
                purchaseItems
            );

            // Act
            var result = await controller.CreatePurchase(purchaseDTO);

            // Assert
            var createdResult = Assert.IsType<CreatedAtActionResult>(result);
            var actualPurchaseDetailDTO = Assert.IsType<PurchaseDetailDTO>(createdResult.Value);

            Assert.Equal(expectedpurchaseDetailDTO, actualPurchaseDetailDTO);
        }

    }

}
