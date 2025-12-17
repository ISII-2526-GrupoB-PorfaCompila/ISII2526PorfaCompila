using AppForMovies.UIT.RentalMovies;
using AppForMovies.UIT.Shared;
using AppForSEII2526.UIT.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AppForSEII2526.UIT.UC_Rental
{
    public class UC_RentalCars_UIT : UC_UIT
    {
        private SelectCarsForRental_PO selectCarsForRental_PO;
        private const int carId1 = 1;

        private string validFrom = DateTime.Today.AddDays(-1).ToString("dd/MM/yyyy");
        private string validTo = DateTime.Today.AddDays(-1).ToString("dd/MM/yyyy");
        private const string carRentingPrice1 = "35";
        private const string carModel1 = "TOYOTA";
        private const string carColor1 = "Blanco";
        private const string carFuelType1 = "Hibrido";
        private const string carManufacturer1 = "TOYOTA";

        private const string carRentingPrice2 = "85";
        private const string carModel2 = "KIA";
        private const string carColor2 = "Gris";
        private const string carFuelType2 = "Diesel";
        private const string carManufacturer2 = "KIA";

        public UC_RentalCars_UIT(ITestOutputHelper output) : base(output)
        {
            Initial_step_opening_the_web_page(); //abre la página.
            selectCarsForRental_PO = new SelectCarsForRental_PO(_driver, _output);
        }


        /////////////////////////////
        ///// Tests del Select //////
        /////////////////////////////

        private void InitialStepsForRentalCars() //Entra en el apartado de rental
        {
            //Precondition_perform_login();
            //we wait for the option of the menu to be visible
            //selectMoviesForRental_PO.WaitForBeingVisible(By.Id("CreateRental"));
            //we click on the menu
            _driver.FindElement(By.Id("CreateRental")).Click();
        }

        //Tests de filtros 
        [Theory]
        [InlineData(carModel1, carRentingPrice1, carColor1, carFuelType1, carManufacturer1, "TOYOTA", "100")]
        [InlineData(carModel1, carRentingPrice1, carColor1, carFuelType1, carManufacturer1, "", "35")]
        [Trait("LevelTesting", "Funcional Testing")]
        public void UC2_Esc2_4_5_filtering(string carModel, string carRentingPrice, string carColor,  string carFuelType, string carManufacturer, string filterModel, string filterRentingPrice)
        {
            //Arrange
            InitialStepsForRentalCars();
            var expectedCars = new List<string[]> { new string[] { carModel, carRentingPrice, carColor, carFuelType, carManufacturer }, };

            //Act
            selectCarsForRental_PO.SearchCars(filterRentingPrice, filterModel, "", "");

            //Assert
            Assert.True(selectCarsForRental_PO.CheckListOfCars(expectedCars));

        }

        //test de error de fechas
        public static IEnumerable<object[]> TestCasesFor_UC2_Esc2_12_13_14_errorindates()
        {
            var allTests = new List<object[]> {
                new object[] { DateTime.Today.AddDays(-1).ToString("dd/MM/yyyy"), DateTime.Today.AddDays(2).ToString("dd/MM/yyyy"), "Errors: Your rental period must be later",  },
                //cannot be checked if datetime is before today, because the next condition is checked before
                new object[] { DateTime.Today.AddDays(-2).ToString("dd/MM/yyyy"), DateTime.Today.AddDays(-1).ToString("dd/MM/yyyy"), "Errors: Your rental period must be later", },
                new object[] { DateTime.Today.AddDays(7).ToString("dd/MM/yyyy"), DateTime.Today.AddDays(5).ToString("dd/MM/yyyy"), "Your rental must end after than its starts", },
            };

            return allTests;
        }
        [Theory]
        [MemberData(nameof(TestCasesFor_UC2_Esc2_12_13_14_errorindates))]
        [Trait("LevelTesting", "Funcional Testing")]
        public void UC2_Esc2_12_13_14_errorindates(string from, string to, string error)
        {
            //Arrange en TestcasesFor
            //Act
            InitialStepsForRentalCars();

            selectCarsForRental_PO.SearchCars("100", "", from, to);

            //Assert
            //this message will be shown if assert fails
            Assert.True(selectCarsForRental_PO.CheckMessageError(error), $"Error in the message box for test {from} - {to}");
        }

        //test de botón no activo
        [Fact]
        [Trait("LevelTesting", "Funcional Testing")]
        public void UC2_Esc3_6_RentingNotavailable()
        {
            //Arrange
            InitialStepsForRentalCars();
            //Act
            selectCarsForRental_PO.SearchCars("100", "", "", "");
            selectCarsForRental_PO.AddCarToRentingCart(carModel1);
            selectCarsForRental_PO.RemoveCarFromRentingCart(carModel1);

            //Assert
            Assert.True(selectCarsForRental_PO.RentingNotAvailable());

        }

        [Fact]
        [Trait("LevelTesting", "Funcional Testing")]
        public void UC2_Esc4_7_ModifySelectedMovies()
        {
            //Arrange
            InitialStepsForRentalCars();

            //Act
            selectCarsForRental_PO.SearchCars("100", "", "", "");
            selectCarsForRental_PO.AddCarToRentingCart(carModel1);
            selectCarsForRental_PO.AddCarToRentingCart(carModel2);
            selectCarsForRental_PO.RemoveCarFromRentingCart(carModel2);

            //Assert                       
            //Comprueba que hay un solo item en el carrito (ergo se ha eliminado carModel2)
            Assert.Equal(1, selectCarsForRental_PO.CountCarsInCart());
        }

        /////////////////////////////
        ///// Tests del Create //////
        /////////////////////////////

        //Test error datos obligatorios
        [Theory]
        [InlineData("", "Martinez", "Calle Calatrava", "The Name field is required.")]
        [InlineData("Laura", "", "Calle Calatrava", "The Surname field is required.")]
        [InlineData("Laura", "Martinez", "", "The DeliveryCarDealer field is required.")]
        [Trait("LevelTesting", "Funcional Testing")]
        public void UC2_Esc5_8_9_10_testingErrorsMandatorydata(string name, string surname, string deliveryAddress, string expectedMessageError)
        {
            var createRental_PO = new CreateRental_PO(_driver, _output);
            //Arrange
            var from = DateTime.Today.AddDays(2).ToString();
            var to = DateTime.Today.AddDays(3).ToString();
            //Act
            InitialStepsForRentalCars();

            selectCarsForRental_PO.SearchCars("100", "", from, to);
            selectCarsForRental_PO.AddCarToRentingCart(carModel1);
            selectCarsForRental_PO.RentCars();
            createRental_PO.FillInRentalInfo(name, surname, deliveryAddress, "Visa");
            createRental_PO.PressRentYourCars();

            //Assert
            //the expected error is shown in the view
            Assert.True(createRental_PO.CheckValidationError(expectedMessageError), $"Expected error: {expectedMessageError}");
        }

        //Test de modificar y guardar datos
        [Fact]
        [Trait("LevelTesting", "Funcional Testing")]
        public void UC2_Esc6_11_ModifyRentalItems()
        {
            //Arrange

            var createRental_PO = new CreateRental_PO(_driver, _output);

            var from = DateTime.Today.AddDays(2).ToString();
            var to = DateTime.Today.AddDays(3).ToString();
            //Act
            InitialStepsForRentalCars();

            selectCarsForRental_PO.SearchCars("100", "", from, to);
            selectCarsForRental_PO.AddCarToRentingCart(carModel1);
            selectCarsForRental_PO.AddCarToRentingCart(carModel2);
            selectCarsForRental_PO.RentCars();
            createRental_PO.PressModifyCars();
            //we remove movietitle2 from the rentingcart
            selectCarsForRental_PO.RemoveCarFromRentingCart(carModel2);
            selectCarsForRental_PO.RentCars();

            //Assert
            //the list of movies must change
            var expectedRentalItems = new List<string[]> { new string[] { carModel1, carManufacturer1, carRentingPrice1 }, };
            Assert.True(createRental_PO.CheckListOfRentalItems(expectedRentalItems));
        }

        //test del flujo básico
        [Theory]
        [InlineData("Laura", "Martinez", "Calle Calatrava", "GooglePay")]
        [InlineData("Laura", "Martinez", "Calle Calatrava", "Paypal")]
        [InlineData("Laura", "Martinez", "Calle Calatrava", "Visa")]

        [Trait("LevelTesting", "Funcional Testing")]
        public void UC2_Esc1_1_2_3_BasicFlow(string name, string surname, string deliveryAddress, string paymentMethod)
        {
            //Arrange

            var createrental = new CreateRental_PO(_driver, _output);
            var detailRental = new DetailRental_PO(_driver, _output);

            var from = DateTime.Today.AddDays(2).ToString();
            var to = DateTime.Today.AddDays(3).ToString();
            var fromDateType = DateTime.Today.AddDays(2);
            var toDateType = DateTime.Today.AddDays(3);

            //Act
            InitialStepsForRentalCars();

            selectCarsForRental_PO.SearchCars("100", "", from, to);
            selectCarsForRental_PO.AddCarToRentingCart(carModel1);
            selectCarsForRental_PO.RentCars();

            createrental.FillInRentalInfo(name, surname, deliveryAddress, paymentMethod);
            createrental.PressRentYourCars();
            createrental.PressOkModalDialog();

            //Assert
            //the expected error is shown in the view
            Assert.True(detailRental.CheckRentalDetail(name, surname,
                deliveryAddress, paymentMethod, DateTime.Now, fromDateType, toDateType, carRentingPrice1 + " €"),
                "Error: detail rental is not as expected");

            var expectedRentalItems = new List<string[]>
                    { new string[] { carModel1, carManufacturer1, carRentingPrice1+" €" }, };

            Assert.True(detailRental.CheckListOfMovies(expectedRentalItems),
                "Error: rental items are not as expected");

        }

    }

}

