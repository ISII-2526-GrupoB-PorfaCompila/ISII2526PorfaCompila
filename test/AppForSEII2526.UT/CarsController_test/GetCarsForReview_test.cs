using AppForSEII2526.API.Controllers;
using AppForSEII2526.API.DTOs;
using AppForSEII2526.API.DTOs.ReviewDTO;
using Humanizer.Localisation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppForSEII2526.UT.CarsController_test
{
    public class GetCarsForReview_test : AppForSEII25264SqliteUT
    {
        public GetCarsForReview_test()
        {
            var models = new List<Model>()
            {
                new Model("FIAT"),
                new Model("KIA"),
                new Model("SEAT")
            };

            var cars = new List<Car>(){
                new Car(models[0], "Fiat", "FIAT", "Gasolina", "Rojo"),
                new Car(models[1], "Kia", "KIA", "Gasoil", "Negro"),
                new Car(models[2], "Seat", "SEAT", "Diesel", "Azul")
            };

            _context.AddRange(models);
            _context.AddRange(cars);
            _context.SaveChanges();
        }


        public static IEnumerable<object[]> TestCasesFor_GetCarsForReview_OK()
        {

            var carDTOs = new List<CarForReviewDTO>() {
                new CarForReviewDTO(1,"FIAT", "Fiat", "FIAT", "Gasolina", "Rojo"),
                new CarForReviewDTO(2,"KIA", "Kia", "KIA", "Gasoil", "Negro"),
                new CarForReviewDTO(3,"SEAT", "Seat", "SEAT", "Diesel", "Azul"),
            };

            var carDTOsTC1 = new List<CarForReviewDTO>() { carDTOs[0], carDTOs[1] , carDTOs[2] }
                    //the GetCarsForReview method returns the cars ordered by model
                    .OrderBy(m => m.Id).ToList();
            var carDTOsTC2 = new List<CarForReviewDTO>() { carDTOs[0] };
            var carDTOsTC3 = new List<CarForReviewDTO>() { carDTOs[1] };


            var allTests = new List<object[]>
            {             //filters to apply - expected cars
                new object[] { null, null, carDTOsTC1 },
                new object[] { "FIAT", null, carDTOsTC2 },
                new object[] { null, "Gasoil", carDTOsTC3 },
            };

            return allTests;
        }

        [Theory]
        [MemberData(nameof(TestCasesFor_GetCarsForReview_OK))]
        [Trait("Database", "WithoutFixture")]
        [Trait("LevelTesting", "Unit Testing")]
        public async Task GetCarsForReview_OK_test(string? filterManufacturer, string? filterFuelType,
            IList<CarForReviewDTO> expectedCars)
        {
            // Arrange
            var controller = new CarsController(_context, null);

            // Act
            var result = await controller.GetCars_ForReview(filterManufacturer, filterFuelType);

            //Assert
            //we check that the response type is OK 
            var okResult = Assert.IsType<OkObjectResult>(result);
            //and obtain the list of cars
            var carDTOsActual = Assert.IsType<List<CarForReviewDTO>>(okResult.Value);
            Assert.Equal(expectedCars, carDTOsActual);

        }
    }
}