using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppForMovies.UIT.Shared;
using AppForSEII2526.UIT.Shared;
//using OpenQA.Selenium.DevTools.V137.Network;

namespace AppForSEII2526.UIT.UC_Purchase
{
    public class UC_PurchaseCars_UIT : UC_UIT
    {
        private SelectCarsForPurchase_PO selectCarsForPurchase_PO;
        private CreatePurchase_PO createPurchase_PO;
        //Cars data
        private const int carId1 = 1;
        private const string model1 = "TOYOTA";
        private const string color1 = "Blanco";
        private const string manufacturer1 = "TOYOTA";
        private const string fuelType1 = "Hibrido";
        private const string carPriceForPurchase1 = "21500";

        private const int carId2 = 2;
        private const string model2 = "KIA";
        private const string color2 = "Gris";
        private const string manufacturer2 = "KIA";
        private const string fuelType2 = "Diesel";
        private const string carPriceForPurchase2 = "32900";


        public UC_PurchaseCars_UIT(ITestOutputHelper output) : base(output)
        {
            Initial_step_opening_the_web_page();
            selectCarsForPurchase_PO = new SelectCarsForPurchase_PO(_driver, _output);
            // Precondicion de inicio de sesion
            //private void Precondition_perform_login() { }

        }
        private void InitialStepsForPurchaseCars()
        {
            // Precondition_perform_login();
            //we wait for the option of the menu to be visible
            selectCarsForPurchase_PO.WaitForBeingVisible(By.Id("CreatePurchase"));
            //we click on the menu
            _driver.FindElement(By.Id("CreatePurchase")).Click();
        }

        [Theory]
        [InlineData(model1, color1, manufacturer1, fuelType1, carPriceForPurchase1, "Blanco", "")]
        [InlineData(model2, color2, manufacturer2, fuelType2, carPriceForPurchase2, "", "KIA")]
        [Trait("LevelTesting", "Funcional Testing")]
        public void UC1_AF1_UC1_3_4_filtering(string model, string color, string manufacturer, string fuelType, string carPriceForPurchase, string filterColor, string filterModel)
        {
            //Arrange
            InitialStepsForPurchaseCars();
            var expectedCars = new List<string[]> { new string[] { model, color, manufacturer, fuelType, carPriceForPurchase }, };

            //Act
            selectCarsForPurchase_PO.SearchCars(filterColor, filterModel);

            //Assert

            Assert.True(selectCarsForPurchase_PO.CheckListOfCars(expectedCars));

        }

        [Fact]
        [Trait("LevelTesting", "Funcional Testing")]
       public void UC1_AF2_UC1_5_PurchasingNotAvailable()
       {
            //Arrange
            InitialStepsForPurchaseCars();
            //Act
            selectCarsForPurchase_PO.AddCarToPurchasingCart(carId1);
            selectCarsForPurchase_PO.RemoveCarFromPurchasingCart(model1);

            //Assert

            Assert.True(selectCarsForPurchase_PO.PurchasingNotAvailable());

       }

        [Fact]
        [Trait("LevelTesting", "Funcional Testing")]
       public void UC1_AF2_UC1_6_ModifyCart()
       {
           //Arrange
           InitialStepsForPurchaseCars();
           //Act
           selectCarsForPurchase_PO.AddCarToPurchasingCart(carId1);
           selectCarsForPurchase_PO.AddCarToPurchasingCart(carId2);

           selectCarsForPurchase_PO.RemoveCarFromPurchasingCart(model2);

           //Assert

           Assert.True(selectCarsForPurchase_PO.CheckCartPrice(carPriceForPurchase1));

       }
        [Theory]
        [InlineData("","Garcia", "Calle de la Universidad 1, Albacete", "The Name field is required.")]
        [InlineData("Carlos","", "Calle de la Universidad 1, Albacete", "The Surname field is required.")]
        [InlineData("Carlos", "Garcia", "", "The DeliveryCarDealer field is required.")]
        [Trait("LevelTesting", "Funcional Testing")]
       public void UC1_AF4_UC1_7_8_9_RequiredErrors(string name, string surname, string deliveryCarDealer, string expectedMessageError) 
       {
           //Arrange
           
           var createPurchase_PO = new CreatePurchase_PO(_driver, _output);
           InitialStepsForPurchaseCars();

           //Act

           selectCarsForPurchase_PO.AddCarToPurchasingCart(carId1);
           selectCarsForPurchase_PO.PurchaseCars();
           createPurchase_PO.FillInPurchaseInfo(name, surname, deliveryCarDealer, "GooglePay");
           createPurchase_PO.FillInPurchaseDescription("Muy bueno", carId1);
           createPurchase_PO.PressPurchaseYourCars();

           //Assert

           Assert.True(createPurchase_PO.CheckValidationError(expectedMessageError), $"Expected error: {expectedMessageError}");
        }
        [Fact]
        [Trait("LevelTesting", "Funcional Testing")]
        public void UC1_AF5_UC1_10_ModifyPurchaseItems()
        {
            //Arrange

            var createPurchase_PO = new CreatePurchase_PO(_driver, _output);
            InitialStepsForPurchaseCars();

            //Act


            selectCarsForPurchase_PO.AddCarToPurchasingCart(carId1);
            selectCarsForPurchase_PO.AddCarToPurchasingCart(carId2);
            selectCarsForPurchase_PO.PurchaseCars();
            createPurchase_PO.PressModifyCars();
            //we remove movietitle2 from the rentingcart
            selectCarsForPurchase_PO.RemoveCarFromPurchasingCart(model2);
            selectCarsForPurchase_PO.PurchaseCars();

            //Assert
            //the list of movies must change
            var expectedPurchaseItems = new List<string[]> { new string[] { model1, color1, carPriceForPurchase1}, };
            Assert.True(createPurchase_PO.CheckListOfPurchaseItems(expectedPurchaseItems));
        }

    }
}
