using AppForSEII2526.API.Controllers;
using AppForSEII2526.API.DTOs.RentalDTO;
using AppForSEII2526.API.DTOs.ReservaDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppForSEII2526.UT.ReservasController_test
{
    public class PostReserva_test : AppForSEII25264SqliteUT
    {
        public PostReserva_test() //constructor de la clase que simula los datos para los test
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
            reserva.Items.Add(new BookingItem(2, 1, "nada", reserva, mantenimientos[0]));
            _context.AddRange(mantenimientos);
            _context.AddRange(tipos);
            _context.Add(user);
            _context.Add(reserva);
            _context.SaveChanges();
        }
        public static IEnumerable<object[]> TestCasesFor_PostReserva() //esto especifica las pruebas que vamos a hacer
        {
            var reservaItems = new List<ReservaItemDTO>() { new ReservaItemDTO(1,"Mant1",50,5,"todo bien") };

            var reservaNoItem = new ReservaForCreateDTO("María", "Alcazar", PaymentMethod.TarjetaDeCredito, DateTime.Today, "334598",  new List<ReservaItemDTO>());
            
            var ReservaApplicationUser = new ReservaForCreateDTO("Usuario@inventado", "Alcazar", PaymentMethod.TarjetaDeCredito, DateTime.Today.AddDays(1),"334598", reservaItems);

            var reservaFromBeforeToday = new ReservaForCreateDTO("María", "Alcazar", PaymentMethod.TarjetaDeCredito, new DateTime(2025, 6, 18),"334598",  reservaItems);

            var allTests = new List<object[]>
            {             //input for createreserva - Error expected                
                new object[] { reservaNoItem, "Error! Necesitas seleccionar al menos 1 mantenimiento para la reserva." },
                new object[] { ReservaApplicationUser, "Error! El nombre de usuario no está registrado." },
                new object[] { reservaFromBeforeToday, "Error! La fecha del mantenimiento no puede ser anterior a hoy." }
            };

            return allTests;
        }
        [Theory]
        [Trait("LevelTesting", "Unit Testing")]
        [Trait("Database", "WithoutFixture")]
        [MemberData(nameof(TestCasesFor_PostReserva))]
        public async Task CreateReserva_Error_test(ReservaForCreateDTO reservaDTO, string errorExpected)
        {
            // Arrange
            _context.ApplicationUsers.Add(new ApplicationUser
            {
                UserName = "María@gmail.com",
                Name = "María" 
            });
            _context.SaveChanges();
            var mock = new Mock<ILogger<ReservasController>>();
            ILogger<ReservasController> logger = mock.Object;

            var controller = new ReservasController(_context, logger);

            // Act
            var result = await controller.CreateReserva(reservaDTO);

            //Assert
            //we check that the response type is BadRequest and obtain the error returned
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            var problemDetails = Assert.IsType<ValidationProblemDetails>(badRequestResult.Value);

            var errorActual = problemDetails.Errors.First().Value[0];

            //we check that the expected error message and actual are the same
            Assert.StartsWith(errorExpected, errorActual);
        }
        [Fact]
        [Trait("LevelTesting", "Unit Testing")]
        [Trait("Database", "WithoutFixture")]
        public async Task CreateReserva_Success_test()
        {
            // Arrange
            var mock = new Mock<ILogger<ReservasController>>();
            ILogger<ReservasController> logger = mock.Object;
            var controller = new ReservasController(_context, logger);

            var reservaDTO = new ReservaForCreateDTO("Fernando", "Alcazar", PaymentMethod.TarjetaDeCredito, DateTime.Today.AddDays(1),"12345", new List<ReservaItemDTO>(){ new ReservaItemDTO(1,"Mant1",70,5,"todo bien")});

            var expectedreservaDetailDTO = new ReservaDetailDTO(2,"Fernando","Alcazar",PaymentMethod.TarjetaDeCredito, DateTime.Today.AddDays(1), new List<ReservaItemDTO>(){ new ReservaItemDTO(1,"Mant1",70,5,"todo bien")});

            // Act
            var result = await controller.CreateReserva(reservaDTO);

            //Assert
            var createdResult = Assert.IsType<CreatedAtActionResult>(result);
            var actualReservaDetailDTO = Assert.IsType<ReservaDetailDTO>(createdResult.Value);

            Assert.Equal(expectedreservaDetailDTO, actualReservaDetailDTO);
        }
    }
}
