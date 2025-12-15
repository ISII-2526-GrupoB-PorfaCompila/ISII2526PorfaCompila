using AppForMovies.UIT.Shared;
using AppForSEII2526.UIT.Shared;
using System;
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

        public UC_RentalCars_UIT(ITestOutputHelper output) : base(output)
        {
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

        [Fact]
        [Trait("LevelTesting", "Funcional Testing")]
        public void UC2_AF1_UC2_4_5_6_filtering()
        {
            //Arrange
            InitialStepsForRentalCars();
            var expectedCars = new List<string[]> { new string[] { carRentingPrice1, carModel1, carColor1, carFuelType1, carManufacturer1 }, };

            //Act
            selectCarsForRental_PO.SearchMovies("", "A3", "", "");

            //Assert

            Assert.True(selectCarsForRental_PO.CheckListOfCars(expectedCars));

        }
    }
}
