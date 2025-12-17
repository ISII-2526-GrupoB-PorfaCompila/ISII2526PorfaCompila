using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppForSEII2526.UIT.UC_Purchase
{
    public class CreatePurchase_PO : PageObject
    {
        private By _nameBy = By.Id("Name");
        private IWebElement _name() => _driver.FindElement(_nameBy);
        private IWebElement _surname() => _driver.FindElement(By.Id("Surname"));
        private IWebElement _deliveryCarDealer() => _driver.FindElement(By.Id("DeliveryCarDealer"));
        private IWebElement _paymentMethod() => _driver.FindElement(By.Id("PaymentMethod"));


        public CreatePurchase_PO(IWebDriver driver, ITestOutputHelper output)
            : base(driver, output)
        {
        }

        public void FillInPurchaseInfo(string name, string surname, string deliveryCarDealer, string paymentMethod)
        {
            WaitForBeingVisible(_nameBy);
            _name().SendKeys(name);
            _surname().SendKeys(surname);
            _deliveryCarDealer().SendKeys(deliveryCarDealer);

            //create select element object 
            SelectElement selectElement = new SelectElement(_paymentMethod());

            //select Action from the dropdown menu
            selectElement.SelectByText(paymentMethod);
        }

        public void FillInPurchaseDescription(string purchaseDescription, int carId)
        {
            _driver.FindElement(By.Id("description_" + carId)).SendKeys(purchaseDescription);
        }

        public void FillInPurchaseQuantity(string purchaseQuantity, int carId)
        {
            _driver.FindElement(By.Id("quantity_" + carId)).SendKeys(purchaseQuantity);
        }

        public void PressPurchaseYourCars()
        {
            WaitForBeingClickable(By.Id("Submit"));
            _driver.FindElement(By.Id("Submit")).Click();
        }



        public void PressModifyCars()
        {
            _driver.FindElement(By.Id("ModifyCars")).Click();
        }

        public bool CheckListOfPurchaseItems(List<string[]> expectedPurchaseItems)
        {
            return CheckBodyTable(expectedPurchaseItems, By.Id("TableOfPurchaseItems"));
        }

        public bool CheckValidationError(string expectedError)
        {
            try
            {
                By errorTextLocator = By.XPath($"//*[contains(text(), '{expectedError}')]");

                WaitForBeingVisible(errorTextLocator);
                return true;
            }
            catch (WebDriverTimeoutException)
            {
                _output.WriteLine($"Timeout: Selenium no encontró ningún elemento visible con el texto '{expectedError}' tras esperar.");
                return false;
            }
        }
    }  
}
