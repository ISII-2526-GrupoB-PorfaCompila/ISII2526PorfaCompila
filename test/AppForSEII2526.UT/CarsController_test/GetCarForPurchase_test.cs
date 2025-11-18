using AppForSEII2526.API.Controllers;
using AppForSEII2526.API.DTOs.PurchaseDTOs;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppForSEII2526.UT.CarsController_test
{
    public class GetCarForPurchase_test : AppForSEII25264SqliteUT
    {

        public GetCarForPurchase_test()
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

            _context.AddRange(models);
            _context.AddRange(cars);
            _context.SaveChanges();
        }

        public static IEnumerable<object[]> TestCasesFor_GetCarsForPurchase_OK() 
        {
            var carsDTOs = new List<CarForPurchaseDTO>() { 
                new CarForPurchaseDTO(1, "FIAT", "Rojo", "FIAT", "Gasolina", 15000),
                new CarForPurchaseDTO(2, "KIA", "Blanco", "KIA", "Eléctrico", 40000),
                new CarForPurchaseDTO(3, "SEAT", "Azul", "SEAT", "Gasoil", 30000)
            };

            var carDTOsTC1 = new List<CarForPurchaseDTO>() { carsDTOs[0], carsDTOs[1], carsDTOs[2] }; // sin filtros
            var carDTOsTC2 = new List<CarForPurchaseDTO>() { carsDTOs[2] };                           // modelo SEAT
            var carDTOsTC3 = new List<CarForPurchaseDTO>() { carsDTOs[0] };                           // color Rojo
            var carDTOsTC4 = new List<CarForPurchaseDTO>();

            var allTests = new List<object[]>
            {             
                new object[] { null,   null,    carDTOsTC1 },  // todos
                new object[] { null,  "SEAT",   carDTOsTC2 },  // solo SEAT
                new object[] { "Rojo", null,    carDTOsTC3 },  // solo FIAT rojo
                new object[] { "Negro", null,   carDTOsTC4 }   // ninguno
            };

            return allTests;
        }

        [Theory]
        [MemberData(nameof(TestCasesFor_GetCarsForPurchase_OK))]
        [Trait("Database", "WithoutFixture")]
        [Trait("LevelTesting", "Unit Testing")]
        public async Task GetCarsForPurchase_OK_test(string? color, string? model, IList<CarForPurchaseDTO> expectedCars) 
        {
            // Arrange
            var controller = new CarsController(_context, null);

            // Act
            var result = await controller.GetCars_ForPurchase(color, model);

            //Assert
            //we check that the response type is OK 
            var okResult = Assert.IsType<OkObjectResult>(result);
            //and obtain the list of movies
            var carDTOsActual = Assert.IsType<List<CarForPurchaseDTO>>(okResult.Value);
            Assert.Equal(expectedCars, carDTOsActual);
        }
    }
}
