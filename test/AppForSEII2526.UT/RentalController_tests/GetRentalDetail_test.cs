using AppForSEII2526.API.DTOs.RentalDTO;
using AppForSEII2526.API.Controllers;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Storage.Json;

namespace AppForSEII2526.UT.RentalController_test
{
    public class GetRentalDetail_test : AppForSEII25264SqliteUT
    {

        public GetRentalDetail_test() //constructor de la clase que simula los datos para los test
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

            var user = new ApplicationUser("Angel", "Barcelo");

            var rental = new Rental(new List<RentalItem>(), 0, "Albacete", user, new DateTime(2025,12,19), 
                new DateTime(2025,11,19), PaymentMethod.Metalico);

            rental.RentalItems.Add(new RentalItem(cars[0], rental, 2));

            _context.AddRange(models);
            _context.AddRange(cars);
            _context.Add(user);
            _context.Add(rental);
            _context.SaveChanges();
        }

        [Fact]
        [Trait("LevelTesting", "Unit Testing")]
        [Trait("Database", "WithoutFixture")]
        public async Task GetRental_NotFound_test()
        {
            // Arrange
            var mock = new Mock<ILogger<RentalController>>();
            ILogger<RentalController> logger = mock.Object;

            var controller = new RentalController(_context, logger);

            // Act
            var result = await controller.GetRentalDetail(0);

            //Assert
            //we check that the response type is OK and obtain the list of cars
            Assert.IsType<NotFoundResult>(result);

        }

        [Fact]
        [Trait("LevelTesting", "Unit Testing")]
        [Trait("Database", "WithoutFixture")]
        public async Task GetRental_Found_test()
        {
            // Arrange
            var mock = new Mock<ILogger<RentalController>>();
            ILogger<RentalController> logger = mock.Object;
            var controller = new RentalController(_context, logger);


            var expectedRental = new RentalDetailDTO("Angel", "Barcelo", "Albacete", new List<RentalItemDTO>(), PaymentMethod.Metalico, 
                new DateTime(2025,12,19), DateTime.Today, new DateTime(2025,11,19));
            expectedRental.RentalItems.Add(new RentalItemDTO(1, "Audi", 2, 49.99, "Audi"));

            // Act 
            var result = await controller.GetRentalDetail(1);

            //Assert
            //we check that the response type is OK and obtain the rental
            var okResult = Assert.IsType<OkObjectResult>(result);
            var rentalDTOActual = Assert.IsType<RentalDetailDTO>(okResult.Value);
            var eq = expectedRental.Equals(rentalDTOActual);
            //we check that the expected and actual are the same
            Assert.Equal(expectedRental, rentalDTOActual);

        }

    }
}
