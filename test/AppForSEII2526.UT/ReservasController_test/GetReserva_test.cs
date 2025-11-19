using AppForSEII2526.API.Controllers;
using AppForSEII2526.API.DTOs.RentalDTO;
using AppForSEII2526.API.DTOs.ReservaDTO;
using AppForSEII2526.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppForSEII2526.UT.ReservasController_test
{
    public class GetReserva_test : AppForSEII25264SqliteUT
    {
        public GetReserva_test() //constructor de la clase que simula los datos para los test
        {
            var mantenimientos = new List<Maintenance>()
            {
                new Maintenance(1,"Mant1",6,70),
                new Maintenance(2,"Mant2",3,35),
                new Maintenance(3,"Mant3",13,58)
            };
            var tipos = new List<MaintenanceType>()
            {
                new MaintenanceType(1,"Motor", mantenimientos[0]),
                new MaintenanceType(2,"Interior", mantenimientos[1]),
                new MaintenanceType(3,"Motor",mantenimientos[2]),
            };
            var user = new ApplicationUser("Fernando", "Alonso");
            var reserva = new Booking(user, "Alcazar", new DateTime(2026, 2, 20), PaymentMethod.TarjetaDeCredito, new List<BookingItem>());
            reserva.Items.Add(new BookingItem(2, 1,"nada",reserva, mantenimientos[0]));
            _context.AddRange(mantenimientos);
            _context.AddRange(tipos);
            _context.Add(user);
            _context.Add(reserva);
            _context.SaveChanges();
        }
        [Fact]
        [Trait("LevelTesting", "Unit Testing")]
        [Trait("Database", "WithoutFixture")]
        public async Task GetReserva_NotFound_test()
        {
            // Arrange
            var mock = new Mock<ILogger<ReservasController>>();
            ILogger<ReservasController> logger = mock.Object;

            var controller = new ReservasController(_context, logger);

            // Act
            var result = await controller.GetReserva(0);

            //Assert
            //we check that the response type is OK and obtain the list of cars
            Assert.IsType<NotFoundResult>(result);
        }
        [Fact]
        [Trait("LevelTesting", "Unit Testing")]
        [Trait("Database", "WithoutFixture")]
        public async Task GetReserva_Found_test()
        {
            // Arrange
            var mock = new Mock<ILogger<ReservasController>>();
            ILogger<ReservasController> logger = mock.Object;
            var controller = new ReservasController(_context, logger);
            var expectedReserva = new ReservaDetailDTO(1, "Fernando", "Alcazar", PaymentMethod.TarjetaDeCredito, new DateTime(2026, 2, 20), new List<ReservaItemDTO>());
            expectedReserva.ReservaItems.Add(new ReservaItemDTO(1, "Mant1", 70, 6, "nada"));

            // Act 
            var result = await controller.GetReserva(1);

            //Assert
            //we check that the response type is OK and obtain the rental
            var okResult = Assert.IsType<OkObjectResult>(result);
            var reservaDTOActual = Assert.IsType<ReservaDetailDTO>(okResult.Value);
            //we check that the expected and actual are the same
            Assert.Equal(expectedReserva.ReservaItems, reservaDTOActual.ReservaItems);;
        }
    }
}
