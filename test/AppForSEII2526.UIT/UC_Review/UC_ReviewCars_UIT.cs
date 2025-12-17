using System;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
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

        private const int id1 = 1;
        private const string modelName1 = "TOYOTA";
        private const string carClass1 = "Utilitario";
        private const string manufacturer1 = "TOYOTA";
        private const string fuelType1 = "Hibrido";
        private const string color1 = "Blanco";

        private const int id2 = 2;
        private const string modelName2 = "VOLKSWAGEN";
        private const string carClass2 = "Compacto";
        private const string manufacturer2 = "VOLKSWAGEN";
        private const string fuelType2 = "Gasolina";
        private const string color2 = "Rojo";

        public UC_ReviewCars_UIT(ITestOutputHelper output) : base(output)
        {
            Initial_step_opening_the_web_page();
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
            selectCarsForReview_PO.WaitForBeingVisible(By.Id("CreateReview"));
            //we click on the menu
            _driver.FindElement(By.Id("CreateReview")).Click();
        }

        [Theory]
        [InlineData(modelName1, carClass1, manufacturer1, fuelType1, color1, "To", "")]
        [InlineData(modelName2, carClass2, manufacturer2, fuelType2, color2, "", "Ga")]
        [Trait("LevelTesting", "Funcional Testing")]
        public void UC4_AF0_UC4_3_4_filtering(string model, string carClass, string manufacturer, string fuelType, string color,
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

        [Fact]
        [Trait("LevelTesting", "Funcional Testing")]

        public void UC4_AF1_UC4_5_ReviewingNotavailable()
        {
            //Arrange
            InitialStepsForReviewCars();
            //Act
            selectCarsForReview_PO.AddCarToReviewingCart(id1);
            selectCarsForReview_PO.RemoveCarFromReviewingCart(id1);

            //Assert

            Assert.True(selectCarsForReview_PO.ReviewingNotAvailable());

        }

        [Fact]
        [Trait("LevelTesting", "Funcional Testing")]
        public void UC4_AF2_UC4_6_ModifySelectedCars()
        {
            //Arrange
            InitialStepsForReviewCars();
            //Act
            selectCarsForReview_PO.AddCarToReviewingCart(id1);
            selectCarsForReview_PO.AddCarToReviewingCart(id2);

            selectCarsForReview_PO.RemoveCarFromReviewingCart(id2);


            //Assert            
            Assert.Equal(1, selectCarsForReview_PO.CountCarsInReviewCart());

            //Para asegurarnos de que el coche es el 1
            //Assert.True(selectCarsForReview_PO.IsCarInReviewCart(id1));
            //Assert.False(selectCarsForReview_PO.IsCarInReviewCart(id2));
        }

        [Theory]
        [InlineData("", "España", "The UserName field is required")]
        [InlineData("carlosg", "", "The Country field is required")]
        [Trait("LevelTesting", "Funcional Testing")]
        public void UC4_AF3_UC4_7_8_testingErrorsMandatorydata(string userName, string country,
            string expectedMessageError)
        {
            //Arrange
            var createreview = new CreateReview_PO(_driver, _output);
            //Act
            InitialStepsForReviewCars();

            selectCarsForReview_PO.AddCarToReviewingCart(id1);
            selectCarsForReview_PO.ReviewCars();
            createreview.FillInReviewInfo(userName, country, "Experto");
            createreview.FillInReviewRating(4, id1);
            createreview.PressReviewYourCars();

            //Assert
            //the expected error is shown in the view
            Assert.True(createreview.CheckValidationError(expectedMessageError), $"Expected error: {expectedMessageError}");
        }

        [Fact]
        [Trait("LevelTesting", "Funcional Testing")]
        public void UC4_AF4_UC4_9_ModifyReviewItems()
        {
            //Arrange
            var createreview = new CreateReview_PO(_driver, _output);

            //Act
            InitialStepsForReviewCars();

            selectCarsForReview_PO.AddCarToReviewingCart(id1);
            selectCarsForReview_PO.AddCarToReviewingCart(id2);
            selectCarsForReview_PO.ReviewCars();
            createreview.PressModifyCars();
            //we remove car2 from the reviewingcart
            selectCarsForReview_PO.RemoveCarFromReviewingCart(id2);
            selectCarsForReview_PO.ReviewCars();

            //Assert
            //the list of movies must change
            var expectedReviewItems = new List<string[]> { new string[] { modelName1, fuelType1, manufacturer1, color1 }, };
            Assert.True(createreview.CheckListOfReviewItems(expectedReviewItems));
        }
    }
}