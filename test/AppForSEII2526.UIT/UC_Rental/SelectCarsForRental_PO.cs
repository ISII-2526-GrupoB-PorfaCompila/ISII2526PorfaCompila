using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium.Support.UI;

namespace AppForSEII2526.UIT.UC_Rental
{
    public class SelectCarsForRental_PO : PageObject
    {
        By inputPrice = By.Id("inputPrice");
        By inputGenre = By.Id("selectGenre");
        By inputFrom = By.Id("fromDate");
        By inputTo = By.Id("toDate");
        By tableOfCarsBy = By.Id("TableOfCars");

        By buttonSearchCars = By.Id("searchCars");
        public SelectCarsForRental_PO(IWebDriver driver, ITestOutputHelper output) : base(driver, output)
        {
        }
        public void SearchMovies(string price, string model, string from, string to)
        {
            //wait for the webelement to be clickable
            WaitForBeingClickable(inputPrice);
            _driver.FindElement(inputPrice).SendKeys(price);
            if (model == "") model = "All";
            SelectElement selectElement = new SelectElement(_driver.FindElement(inputGenre));
            selectElement.SelectByText(model);

            _driver.FindElement(buttonSearchCars).Click();

            if (from != "")
                _driver.FindElement(inputFrom).SendKeys(from);
            if (to != "")
                _driver.FindElement(inputTo).SendKeys(to);

        }

        public bool CheckListOfCars(List<string[]> expectedCars)
        {
            return CheckBodyTable(expectedCars, tableOfCarsBy);
        }

    }
}
