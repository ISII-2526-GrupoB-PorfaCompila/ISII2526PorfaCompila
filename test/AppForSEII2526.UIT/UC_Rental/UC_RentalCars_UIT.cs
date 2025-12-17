using AppForMovies.UIT.Shared;
using AppForSEII2526.UIT.Shared;
using System;
using System.Runtime.CompilerServices;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppForSEII2526.UIT.UC_Rental
{
    public class UC_RentalCars_UIT : UC_UIT
    {
        private SelectCarsForRental_PO selectCarsForRental_PO;
        private const int carId1 = 1;
        private const string carRentingPrice1 = "60";
        private const string carModel1 = "A3";
        private const string carColor1 = "Black";
        private const string carFuelType1 = "Petrol";
        private const string carManufacturer1 = "Audi";

        private const string carRentingPrice2 = "45";
        private const string carModel2 = "Corolla";
        private const string carColor2 = "Silver";
        private const string carFuelType2 = "Petrol";
        private const string carManufacturer2 = "Toyota";

        public UC_RentalCars_UIT(ITestOutputHelper output) : base(output)
        {
            Initial_step_opening_the_web_page();
            selectCarsForRental_PO = new SelectCarsForRental_PO(_driver, _output);
        }

        //NO aplica porque no estamos usando login
        //private void Precondition_perform_login()
        //{
        //    Perform_login("elena@uclm.es", "Password1234%");
        //}

        private void InitialStepsForRentalCars()
        {
            //Precondition_perform_login();
            //we wait for the option of the menu to be visible
            //selectMoviesForRental_PO.WaitForBeingVisible(By.Id("CreateRental"));
            //we click on the menu
            _driver.FindElement(By.Id("CreateRental")).Click();
        }

        //Tests de filtros
        [Theory]
        [InlineData(carRentingPrice1, carModel1, carColor1, carFuelType1, carManufacturer1, "A3", "")]
        [InlineData(carRentingPrice2, carModel2, carColor2, carFuelType2, carManufacturer2, "", "45")]
        [Trait("LevelTesting", "Funcional Testing")]
        public void UC2_Esc2_UC2_4_5_filtering(string carRentingPrice, string carModel, string carColor, string carFuelType, string carManufacturer, string filterModel, string filterRentingPrice)
        {
            //Arrange
            InitialStepsForRentalCars();
            var expectedCars = new List<string[]> { new string[] { carRentingPrice, carModel, carColor, carFuelType, carManufacturer }, };

            //Act
            selectCarsForRental_PO.SearchCars(filterRentingPrice, filterModel, "", "");

            //Assert
            Assert.True(selectCarsForRental_PO.CheckListOfCars(expectedCars));

        }

        //test de error de fechas ---------------------- Falta clickar el botón.
        public static IEnumerable<object[]> TestCasesFor_Esc2_UC2_6_errorindates()
        {
            var allTests = new List<object[]> {
                new object[] { DateTime.Today.AddDays(-1).ToString("dd/MM/yyyy"), DateTime.Today.AddDays(2).ToString("dd/MM/yyyy"), "Your rental period must be later",  },
                //cannot be checked if datetime is before today, because the next condition is checked before
                new object[] { DateTime.Today.AddDays(-2).ToString("dd/MM/yyyy"), DateTime.Today.AddDays(-1).ToString("dd/MM/yyyy"), "Your rental period must be later", },
                new object[] { DateTime.Today.AddDays(7).ToString("dd/MM/yyyy"), DateTime.Today.AddDays(5).ToString("dd/MM/yyyy"), "Your rental must end after than its starts", },
            };

            return allTests;
        }
        [Theory]
        [MemberData(nameof(TestCasesFor_Esc2_UC2_6_errorindates))]
        [Trait("LevelTesting", "Funcional Testing")]
        public void UC2_Esc2_UC2_6_errorindates(string from, string to, string error)
        {
            //Arrange en TestcasesFor
            //Act
            InitialStepsForRentalCars();

            selectCarsForRental_PO.SearchCars("100", "", from, to);
            selectCarsForRental_PO.AddMovieToRentingCart(carModel1);

            //Assert
            //this message will be shown if assert fails
            Assert.True(selectCarsForRental_PO.CheckMessageError(error), $"Error in the message box for test {from} - {to}");
        }

        //test de botón no activo
        [Fact]
        [Trait("LevelTesting", "Funcional Testing")]
        public void UC2_Esc3_UC2_4_5_RentingNotavailable()
        {
            //Arrange
            InitialStepsForRentalCars();
            //Act
            selectCarsForRental_PO.AddMovieToRentingCart(carModel1);
            selectCarsForRental_PO.RemoveMovieFromRentingCart(carModel1);

            //Assert
            Assert.True(selectCarsForRental_PO.RentingNotAvailable());

        }

        //PACOOOOOO
        //[Fact]
        //[Trait("LevelTesting", "Funcional Testing")]
        //public void UC2_10_AF3_ModifySelectedMovies()
        //{
        //    //Arrange

        //    var from = DateTime.Today.AddDays(2);
        //    var to = DateTime.Today.AddDays(3);
        //    //Act
        //    InitialStepsForRentalCars();

        //    selectCarsForRental_PO.SearchCars("100", "", "", "");
        //    selectCarsForRental_PO.AddMovieToRentingCart(carModel1);
        //    selectCarsForRental_PO.AddMovieToRentingCart(carModel2);
        //    selectCarsForRental_PO.RemoveMovieFromRentingCart(carModel2);


        //    //Assert            
        //    Assert.True(selectCarsForRental_PO.CheckShoppingCart(carRentingPrice1));
        //}

    }

}

