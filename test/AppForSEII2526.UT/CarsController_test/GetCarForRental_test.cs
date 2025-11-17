using AppForSEII2526.API.DTOs.RentalDTO;
using AppForSEII2526.API.Controllers;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppForSEII2526.UT.CarsController_test
{
    public class GetCarForRental_test : AppForSEII25264SqliteUT
    {

        public GetCarForRental_test() //constructor de la clase que simula los datos para los test
        {

            var models = new List<Model>() //primero rellenamos la tabla de modelos de coches.
            {
                new Model("Audi"),
                new Model("BMW"),
            };

            var cars = new List<Car>()
            {
                new Car(1, models[0], "negro", "Audi", "diesel", 49.99),
                new Car(2, models[0], "blanco", "Audi", "gasoil", 90.00),
                new Car(3, models[1], "blanco", "BMW", "gasoil", 300.00)
            };

            _context.AddRange(models);
            _context.AddRange(cars);
            _context.SaveChanges();
        }

        public static IEnumerable<object[]> TestCasesFor_GetCarsForRental_OK() //esto especifica las pruebas que vamos a hacer
        {
            var carsDTOs = new List<CarForRentalDTO>() { //GetCarsForRental devuelve DTOs, aquí los creamos a mano como si devolviese todos los coches.
                new CarForRentalDTO(1, "Audi", "negro", "Audi", "diesel", 49.99),
                new CarForRentalDTO(2, "Audi", "blanco", "Audi", "gasoil", 90.00),
                new CarForRentalDTO(3, "BMW", "blanco", "BMW", "gasoil", 300.00)
            };

            var carDTOsTC1 = new List<CarForRentalDTO>() { carsDTOs[0], carsDTOs[1], carsDTOs[2] };
            var carDTOsTC2 = new List<CarForRentalDTO>() { carsDTOs[2] };
            var carDTOsTC3 = new List<CarForRentalDTO>() { carsDTOs[0], carsDTOs[1] };
            var carDTOsTC4 = new List<CarForRentalDTO>() {  };

            var allTests = new List<object[]>
            {             //filters to apply - expected movies
                new object[] { null, null, carDTOsTC1 },
                new object[] { null, "BMW", carDTOsTC2 },
                new object[] { 100.00, null, carDTOsTC3 },
                new object[] { 10.00, null, carDTOsTC4 }
            };

            return allTests;
        }

        [Theory]
        [MemberData(nameof(TestCasesFor_GetCarsForRental_OK))]
        [Trait("Database", "WithoutFixture")]
        [Trait("LevelTesting", "Unit Testing")]
        public async Task GetMoviesForRental_OK_test(double? rentingPrice, string? model, IList<CarForRentalDTO> expectedCars) //esto es esto { null, null, movieDTOsTC1 }
        {
            // Arrange
            var controller = new CarsController(_context, null);

            // Act
            var result = await controller.GetCars_ForRental(rentingPrice, model);

            //Assert
            //we check that the response type is OK 
            var okResult = Assert.IsType<OkObjectResult>(result);
            //and obtain the list of movies
            var carDTOsActual = Assert.IsType<List<CarForRentalDTO>>(okResult.Value);
            Assert.Equal(expectedCars, carDTOsActual);

        }

    }
}



