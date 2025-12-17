using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppForSEII2526.UIT.UC_Purchase
{
    public class DetailPurchase_PO : PageObject
    {
        public DetailPurchase_PO(IWebDriver driver, ITestOutputHelper output) : base(driver, output)
        {
        }

        public bool CheckPurchaseDetail(string name, string surname, string deliveryCarDealer,
            DateTime purchaseDate, string totalprice)
        {
            WaitForBeingVisible(By.Id("TotalPrice"));
            bool result = true;
            result = result && _driver.FindElement(By.Id("NameSurname")).Text.Contains(name);
            result = result && _driver.FindElement(By.Id("NameSurname")).Text.Contains(surname);
            result = result && _driver.FindElement(By.Id("DeliveryAddress")).Text.Contains(deliveryCarDealer);
            result = result && _driver.FindElement(By.Id("TotalPrice")).Text.Contains(totalprice);

            var actualPurchaseDate = DateTime.Parse(_driver.FindElement(By.Id("PurchaseDate")).Text);
            result = result && ((actualPurchaseDate - purchaseDate) < new TimeSpan(0, 1, 0));


            return result;

        }

        public bool CheckListOfMovies(List<string[]> expectedPurchaseItems)
        {
            return CheckBodyTable(expectedPurchaseItems, By.Id("PurchasedCars"));
        }
    }
}
