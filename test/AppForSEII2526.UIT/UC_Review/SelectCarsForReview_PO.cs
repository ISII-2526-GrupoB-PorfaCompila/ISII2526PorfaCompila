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
    }
}