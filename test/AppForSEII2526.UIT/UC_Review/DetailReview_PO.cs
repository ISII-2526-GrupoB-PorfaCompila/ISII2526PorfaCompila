using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppForSEII2526.UIT.UC_Review
{
    public class DetailReview_PO : PageObject
    {
        public DetailReview_PO(IWebDriver driver, ITestOutputHelper output) : base(driver, output)
        {
        }

        public bool CheckReviewDetail(string userName, string country, string driverType,
            DateTime created)
        {
            var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(5));
            wait.Until(d => d.FindElement(By.Id("Created")).Displayed);

            bool result = true;
            result = result && _driver.FindElement(By.Id("UserName")).Text.Contains(userName);
            result = result && _driver.FindElement(By.Id("Country")).Text.Contains(country);
            result = result && _driver.FindElement(By.Id("DriverType")).Text.Contains(driverType);

            var actualReviewDate = DateTime.Parse(_driver.FindElement(By.Id("Created")).Text);
            result = result && ((actualReviewDate - created) < new TimeSpan(0, 1, 0));

            return result;
        }

        public bool CheckListOfCars(List<string[]> expectedReviewItems)
        {
            return CheckBodyTable(expectedReviewItems, By.Id("ReviewedCars"));
        }
    }
}