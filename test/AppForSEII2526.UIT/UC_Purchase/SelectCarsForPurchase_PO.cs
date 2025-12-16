using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium.Support.UI;

namespace AppForSEII2526.UIT.UC_Purchase  
{
    public class SelectCarsForPurchase_PO : PageObject
    {
        By inputColor = By.Id("inputColor");
        By inputModel = By.Id("selectModel");
        By buttonSearchCars = By.Id("searchCars");
        By tableOfCarsBy = By.Id("TableOfCars");
        By buttonPurchaseCars = By.Id("purchaseCarButton");

        public SelectCarsForPurchase_PO(IWebDriver driver, ITestOutputHelper output) : base(driver, output)
        {
        }
        public void SearchCars(string color, string model)
        {
            //wait for the webelement to be clickable
            WaitForBeingClickable(inputColor);
            _driver.FindElement(inputColor).SendKeys(color);

            if (model == "") model = "All";
            SelectElement selectElement = new SelectElement(_driver.FindElement(inputModel));
            selectElement.SelectByText(model);

            _driver.FindElement(buttonSearchCars).Click();


        }

        public void AddCarToPurchasingCart(int id)
        {
            WaitForBeingClickable(By.Id("CarToPurchase_" + id));

            _driver.FindElement(By.Id("CarToPurchase_" + id)).Click();
        }

        public void RemoveCarFromPurchasingCart(string model)
        {
            WaitForBeingClickable(By.Id("removeCar_" + model));
            _driver.FindElement(By.Id("removeCar_" + model)).Click();
        }

        public bool PurchasingNotAvailable()
        {
            //the button is not Displayed=hidden

            return _driver.FindElement(buttonPurchaseCars).Displayed == false;
        }
        public bool CheckCartPrice()
        {
            //the button is not Displayed=hidden

            return _driver.FindElement(buttonPurchaseCars).Displayed == false;
            return _showPurchasingCartButton().Text.Contains(price);
        }


        public bool CheckListOfCars(List<string[]> expectedCars)
        {

            return CheckBodyTable(expectedCars, tableOfCarsBy);
        }
    }
}