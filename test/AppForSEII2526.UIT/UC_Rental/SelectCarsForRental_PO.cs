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
        By errorShownBy = By.Id("ErrorsShown");
        By buttonRentCars = By.Id("rentCarButton");

        By buttonSearchCars = By.Id("searchCars");
        public SelectCarsForRental_PO(IWebDriver driver, ITestOutputHelper output) : base(driver, output)
        {
        }
        public void SearchCars(string price, string model, string from, string to)
        {
            //_driver.FindElement(inputPrice).Clear();
            //wait for the webelement to be clickable
            WaitForBeingClickable(inputPrice);
            _driver.FindElement(inputPrice).SendKeys(price);
            if (model == "") model = "All";
            SelectElement selectElement = new SelectElement(_driver.FindElement(inputGenre));
            selectElement.SelectByText(model);
            if (from != "")
                _driver.FindElement(inputFrom).SendKeys(from);
            if (to != "")
                _driver.FindElement(inputTo).SendKeys(to);

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
            return actualErrorShown.Text.Contains(errorMessage); //comprueba si son iguales el actualErrorShown y el errorMessage
        }

        public void AddCarToRentingCart(string carModel)
        {
            WaitForBeingClickable(By.Id("carToRent_" + carModel));
            _driver.FindElement(By.Id("carToRent_" + carModel)).Click();
        }

        public void RemoveCarFromRentingCart(string carModel)
        {
            WaitForBeingClickable(By.Id("removeCar_" + carModel));
            _driver.FindElement(By.Id("removeCar_" + carModel)).Click();
        }

        public bool RentingNotAvailable()
        {
            //the button is not Displayed=hidden

            return _driver.FindElement(buttonRentCars).Displayed == false;
        }

        public void RentCars()
        {
            WaitForBeingClickable(buttonRentCars);
            _driver.FindElement(buttonRentCars).Click();
        }

        //cuenta todos los elementos cuya etiqueta empiece por 'removeCar_'
        public int CountCarsInCart()
        {
            return _driver.FindElements(By.CssSelector("[id^='removeCar_']")).Count;
        }



    }
}
