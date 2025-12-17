using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppForSEII2526.UIT.UC_Review
{
    public class CreateReview_PO : PageObject
    {

        private By userNameBy = By.Id("UserName");
        private IWebElement _userName() => _driver.FindElement(userNameBy);
        private IWebElement _country() => _driver.FindElement(By.Id("Country"));
        private IWebElement _driverType() => _driver.FindElement(By.Id("DriverType"));




        public CreateReview_PO(IWebDriver driver, ITestOutputHelper output)
            : base(driver, output)
        {
        }

        public void FillInReviewInfo(string userName, string country, string driverType)
        {
            WaitForBeingVisible(userNameBy);
            _userName().SendKeys(userName);
            _country().SendKeys(country);

            //create select element object 
            SelectElement selectElement = new SelectElement(_driverType());

            //select Action from the dropdown menu
            selectElement.SelectByText(driverType);
        }

        public void FillInReviewDescription(string reviewDescription, int carId)
        {
            _driver.FindElement(By.Id("description_" + carId)).SendKeys(reviewDescription);
        }

        public void FillInReviewRating(int rating, int carId)
        {
            _driver.FindElement(By.Id("rating_" + carId)).SendKeys(rating.ToString());
        }


        public void PressReviewYourCars()
        {
            _driver.FindElement(By.Id("Submit")).Click();
        }



        public void PressModifyCars()
        {
            _driver.FindElement(By.Id("ModifyCars")).Click();
        }

        public bool CheckListOfReviewItems(List<string[]> expectedReviewItems)
        {
            return CheckBodyTable(expectedReviewItems, By.Id("TableOfReviewItems"));
        }

        public bool CheckValidationError(string expectedError)
        {
            return _driver.PageSource.Contains(expectedError);
        }

    }
}
