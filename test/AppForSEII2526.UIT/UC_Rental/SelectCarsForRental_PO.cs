using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppForSEII2526.UIT.UC_Rental
{
    public class SelectCarsForRental_PO : PageObject
    {
        By inputPrice = By.Id("inputPrice");
        By inputGenre = By.Id("selectGenre");
        By buttonSearchCars = By.Id("searchCars");
        public SelectCarsForRental_PO(IWebDriver driver, ITestOutputHelper output) : base(driver, output)
        {
        }
        public void SearchMovies(string title)
        {
            //wait for the webelement to be clickable
            WaitForBeingClickable(inputPrice);
            _driver.FindElement(inputPrice).SendKeys(title);
            _driver.FindElement(buttonSearchCars).Click();


        }
    }
}
