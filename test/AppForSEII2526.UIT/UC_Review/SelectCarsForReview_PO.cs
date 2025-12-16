using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium.Support.UI;

namespace AppForSEII2526.UIT.UC_Review
{
    public class SelectCarsForReview_PO : PageObject
    {
        By inputManufacturer = By.Id("manufacturer");
        By inputFuelType = By.Id("fuelType");
        By buttonSearchCars = By.Id("searchCars");
        By tableOfCarsBy = By.Id("TableOfCars");
        By errorShownBy = By.Id("ErrorsShown");
        By buttonReviewCars = By.Id("reviewCarButton");
        public SelectCarsForReview_PO(IWebDriver driver, ITestOutputHelper output) : base(driver, output)
        {
        }
        public void SearchCars(string manufacturer, string fuelType)
        {
            //wait for the webelement to be clickable
            WaitForBeingClickable(inputManufacturer);
            _driver.FindElement(inputManufacturer).SendKeys(manufacturer);
            _driver.FindElement(inputFuelType).SendKeys(fuelType);
            _driver.FindElement(buttonSearchCars).Click();
        }

        public bool CheckListOfCars(List<string[]> expectedCars)
        {

            return CheckBodyTable(expectedCars, tableOfCarsBy);
        }

        public bool CheckMessageError(string errorMessage)
        {
            IWebElement actualErrorShown = _driver.FindElement(errorShownBy);
            _output.WriteLine($"actual Message shown:{actualErrorShown.Text}");
            return actualErrorShown.Text.Contains(errorMessage);
        }

        public void AddCarToReviewingCart(int carId)
        {
            WaitForBeingClickable(By.Id("carToReview_" + carId));
            _driver.FindElement(By.Id("carToReview_" + carId)).Click();
        }

        public void RemoveCarFromReviewingCart(int carId)
        {
            WaitForBeingClickable(By.Id("removeCar_" + carId));
            _driver.FindElement(By.Id("removeCar_" + carId)).Click();
        }

        public bool ReviewingNotAvailable()
        {
            //the button is not Displayed=hidden

            return _driver.FindElement(buttonReviewCars).Displayed == false;
        }

        public int CountCarsInReviewCart()
        {
            return _driver.FindElements(By.CssSelector("button[id^='removeCar_']")).Count;
        }

        // Método para verificar si un coche específico está en el carrito
        /*public bool IsCarInReviewCart(int carId)
        {
            return _driver.FindElements(By.Id("removeCar_" + carId)).Count > 0;
        }*/
    }
}