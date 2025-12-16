using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppForMovies.UIT.Shared;
using AppForSEII2526.UIT.Shared;

namespace AppForSEII2526.UIT.UC_Review
{
    public class UC_ReviewCars_UIT : UC_UIT
    {
        private SelectCarsForReview_PO selectCarsForReview_PO;

        private const string modelName1 = "TOYOTA";
        private const string carClass1 = "Utilitario";
        private const string manufacturer1 = "TOYOTA";
        private const string fuelType1 = "Hibrido";
        private const string color1 = "Blanco";

        private const string modelName2 = "VOLKSWAGEN";
        private const string carClass2 = "Compacto";
        private const string manufacturer2 = "VOLKSWAGEN";
        private const string fuelType2 = "Gasolina";
        private const string color2 = "Rojo";

        public UC_ReviewCars_UIT(ITestOutputHelper output) : base(output)
        {
            selectCarsForReview_PO = new SelectCarsForReview_PO(_driver, _output);
        }

        /*private void Precondition_perform_login()
        {
            Perform_login("elena@uclm.es", "Password1234%");
        }*/


        private void InitialStepsForReviewCars()
        {
            //Precondition_perform_login();
            //we wait for the option of the menu to be visible
            //selectCarsForReview_PO.WaitForBeingVisible(By.Id("CreateReview"));
            //we click on the menu
            _driver.FindElement(By.Id("CreateReview")).Click();
        }

        [Theory]
        [InlineData(modelName1, carClass1, manufacturer1, fuelType1, color1, "To", "")]
        [InlineData(modelName2, carClass2, manufacturer2, fuelType2, color2, "", "Ga")]
        [Trait("LevelTesting", "Funcional Testing")]
        public void UC2_AF1_UC2_4_5_6_filtering(string model, string carClass, string manufacturer, string fuelType, string color,
            string filterManufacturer, string filterFuelType)
        {
            //Arrange
            InitialStepsForReviewCars();
            var expectedCars = new List<string[]> { new string[] { model, carClass, manufacturer, fuelType, color }, };

            //Act
            selectCarsForReview_PO.SearchCars(filterManufacturer, filterFuelType);

            //Assert

            Assert.True(selectCarsForReview_PO.CheckListOfCars(expectedCars));

        }

        public static IEnumerable<object[]> TestCasesFor_UC2_4_5_AF2_errorindates()
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
        [MemberData(nameof(TestCasesFor_UC2_4_5_AF2_errorindates))]
        [Trait("LevelTesting", "Funcional Testing")]
        public void UC2_7_8_9_AF2_errorindates(string from, string to, string error)
        {
            //Arrange


            //Act
            InitialStepsForRentalMovies();

            selectMoviesForRental_PO.SearchMovies("", "", from, to);

            //Assert

            //this message will be shown if assert fails
            Assert.True(selectMoviesForRental_PO.CheckMessageError(error), $"Error in the message box for test {from} - {to}");

        }
    }
}