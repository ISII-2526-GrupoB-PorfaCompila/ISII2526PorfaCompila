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
        public void UC1_AF2_UC1_6_ModifyCart()
        {
            //Arrange
            InitialStepsForPurchaseCars();
            //Act
            selectCarsForPurchase_PO.AddCarToPurchasingCart(carId1);
            selectCarsForPurchase_PO.AddCarToPurchasingCart(carId2);
            selectCarsForPurchase_PO.RemoveCarFromPurchasingCart(model2);

            //Assert

            Assert.True(selectCarsForPurchase_PO.PurchasingNotAvailable());

        }
    }
}
