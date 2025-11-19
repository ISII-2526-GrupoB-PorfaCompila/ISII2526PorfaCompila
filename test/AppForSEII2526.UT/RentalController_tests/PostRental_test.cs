using AppForSEII2526.API.Controllers;
using AppForSEII2526.API.DTOs.RentalDTO;


namespace AppForSEII2526.UT.CarsController_test
{
    public class PostRental_test : AppForSEII25264SqliteUT
    {

        private const string _userName = "Angel";
        private const string _userSurname = "Barcelo";
        private const string _delivery = "Concesionario1";
        private const PaymentMethod _payment = PaymentMethod.GooglePay;
        private const string _manufacturer = "FabricaCoches";

        private const string _modelo1 = "Audi";
        private const string _modelo2 = "BMW";
        private const string _color = "Blanco";
        private const string _combustible = "gasoil";
        private const double _precio = 3000;

        public PostRental_test() //constructor de la clase que simula los datos para los test
        {

            var models = new List<Model>() //primero rellenamos la tabla de modelos de coches.
            {
                new Model(_modelo1),
                new Model(_modelo2)
            };

            var cars = new List<Car>()
            {
                new Car(1, models[0], _color, _manufacturer, _combustible, _precio),
                new Car(2, models[1], _color, _manufacturer, _combustible, _precio),
            };

            var user = new ApplicationUser(_userName, _userSurname);

            var rental = new Rental(new List<RentalItem>(), 6000, _delivery, new ApplicationUser(_userName, _userSurname), 
                DateTime.Today.AddDays(4), DateTime.Today.AddDays(2), _payment);

            _context.Add(user);
            _context.AddRange(models);
            _context.AddRange(cars);
            _context.Add(rental);
            _context.SaveChanges();
        }

        public static IEnumerable<object[]> TestCasesFor_PostRental() //esto especifica las pruebas que vamos a hacer
        {

            var rentalNoItem = new RentalForCreateDTO(_userName, _userSurname, _delivery, new List<RentalItemDTO>(), 
                _payment, DateTime.Today.AddDays(4), DateTime.Today.AddDays(2));

            var rentalItems = new List<RentalItemDTO>() { new RentalItemDTO(1, _manufacturer, 2, _precio, _modelo1 ), 
                new RentalItemDTO(1, _manufacturer, 2, _precio, _modelo2) };

            var rentalFromBeforeToday = new RentalForCreateDTO(_userName, _userSurname, _delivery, rentalItems, _payment,
                DateTime.Today.AddDays(4), DateTime.Today.AddDays(-1));

            var rentalToBeforeFrom = new RentalForCreateDTO(_userName, _userSurname, _delivery, rentalItems, _payment,
                DateTime.Today.AddDays(4), DateTime.Today.AddDays(8));

            var RentalApplicationUser = new RentalForCreateDTO("usuario inventado", _userSurname, _delivery, rentalItems, _payment,
                DateTime.Today.AddDays(4), DateTime.Today.AddDays(2));


            var allTests = new List<object[]>
            {             //input for createpurchase - Error expected
                new object[] { rentalNoItem, "Error! You must include at least one car to be rented" },
                new object[] { rentalFromBeforeToday, "Error! Your rental date must start later than today" },
                new object[] { rentalToBeforeFrom, "Error! Your rental must end later than it starts" },
                new object[] { RentalApplicationUser, "Error! That user is not registered" },
            };

            return allTests;
        }

        [Theory]
        [Trait("LevelTesting", "Unit Testing")]
        [Trait("Database", "WithoutFixture")]
        [MemberData(nameof(TestCasesFor_PostRental))]
        public async Task CreateRental_Error_test(RentalForCreateDTO rentalDTO, string errorExpected)
        {
            // Arrange
            var mock = new Mock<ILogger<RentalController>>();
            ILogger<RentalController> logger = mock.Object;

            var controller = new RentalController(_context, logger);

            // Act
            var result = await controller.CreateRental(rentalDTO);

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
        public async Task CreateRental_Success_test()
        {
            // Arrange
            var mock = new Mock<ILogger<RentalController>>();
            ILogger<RentalController> logger = mock.Object;
            var controller = new RentalController(_context, logger);

            DateTime to = DateTime.Today.AddDays(7);
            DateTime from = DateTime.Today.AddDays(6);

            var rentalDTO = new RentalForCreateDTO(_userName, _userSurname, _delivery,
                new List<RentalItemDTO>() { new RentalItemDTO(1, _manufacturer, 2, _precio, _modelo1 ),
                new RentalItemDTO(2, _manufacturer, 2, _precio, _modelo2) },
                _payment, to, from);

            var expectedrentalDetailDTO = new RentalDetailDTO(_userName, _userSurname, _delivery, 
                new List<RentalItemDTO>() { new RentalItemDTO(1, _manufacturer, 2, _precio, _modelo1 ),
                new RentalItemDTO(2, _manufacturer, 2, _precio, _modelo2) },
                _payment, to, DateTime.Today, from);

            // Act
            var result = await controller.CreateRental(rentalDTO);

            //Assert
            //we check that the response type is BadRequest and obtain the error returned
            var createdResult = Assert.IsType<CreatedAtActionResult>(result);
            var actualRentalDetailDTO = Assert.IsType<RentalDetailDTO>(createdResult.Value);

            Assert.Equal(expectedrentalDetailDTO, actualRentalDetailDTO);

        }

    }
}
