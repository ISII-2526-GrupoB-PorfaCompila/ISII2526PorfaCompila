using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppForSEII2526.UIT.UC_Rental
{
    public class CreateRental_PO : PageObject
    {

        private By nameBy = By.Id("Name");
        private By SurnameBy = By.Id("Surname");
        private IWebElement _name() => _driver.FindElement(nameBy);
        private IWebElement _surname() => _driver.FindElement(SurnameBy);
        private IWebElement _deliveryAddress() => _driver.FindElement(By.Id("DeliveryAddress"));
        private IWebElement _paymentMethod() => _driver.FindElement(By.Id("PaymentMethod"));




        public CreateRental_PO(IWebDriver driver, ITestOutputHelper output)
            : base(driver, output)
        {
        }

        public void FillInRentalInfo(string name, string surname, string deliveryAddress, string paymentMethod)
        {
            WaitForBeingVisible(nameBy);
            _name().SendKeys(name);
            _surname().SendKeys(surname);
            _deliveryAddress().SendKeys(deliveryAddress);

            //create select element object 
            SelectElement selectElement = new SelectElement(_paymentMethod());

            //select Action from the dropdown menu
            selectElement.SelectByText(paymentMethod);
        }

        public void FillInRentalQuantity(string rentalQuantity, int carId)
        {
            _driver.FindElement(By.Id("quantity_" + carId)).SendKeys(rentalQuantity);
        }

        public void PressRentYourCars()
        {
            _driver.FindElement(By.Id("Submit")).Click();
        }

        public void PressModifyCars()
        {
            _driver.FindElement(By.Id("ModifyCars")).Click();
        }

        public bool CheckListOfRentalItems(List<string[]> expectedRentalItems)
        {
            return CheckBodyTable(expectedRentalItems, By.Id("TableOfRentalItems"));
        }

        public bool CheckValidationError(string expectedError)
        {
            return _driver.PageSource.Contains(expectedError);
        }

    }
}