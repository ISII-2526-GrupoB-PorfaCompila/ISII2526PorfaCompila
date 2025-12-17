using AppForSEII2526.API.Controllers;
using AppForSEII2526.API.DTOs;
using AppForSEII2526.API.DTOs.RentalDTO;
using AppForSEII2526.API.DTOs.ReviewDTO;
using Humanizer.Localisation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppForSEII2526.UT.ReviewsController_test
{
    public class GetReviews_test : AppForSEII25264SqliteUT
    {
        public GetReviews_test()
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

            ApplicationUser user = new ApplicationUser("mperez");

            var review = new Review(user, "España", new DateTime(2025, 11, 17), DriverTypes.Novato, new List<ReviewItem>());
            review.ReviewItems.Add(new ReviewItem(review, cars[0], 4, "Excelente"));

            _context.ApplicationUsers.Add(user);
            _context.AddRange(models);
            _context.AddRange(cars);
            _context.Add(review);
            _context.SaveChanges();
        }

        [Fact]
        [Trait("Database", "WithoutFixture")]
        [Trait("LevelTesting", "Unit Testing")]
        public async Task GetRental_NotFound_test()
        {
            // Arrange
            var mock = new Mock<ILogger<ReviewsController>>();
            ILogger<ReviewsController> logger = mock.Object;

            var controller = new ReviewsController(_context, logger);

            // Act
            var result = await controller.GetReview(0);

            //Assert
            //we check that the response type is OK and obtain the list of cars
            Assert.IsType<NotFoundResult>(result);

        }

        [Fact]
        [Trait("LevelTesting", "Unit Testing")]
        [Trait("Database", "WithoutFixture")]
        public async Task GetReview_Found_test()
        {
            // Arrange
            var mock = new Mock<ILogger<ReviewsController>>();
            ILogger<ReviewsController> logger = mock.Object;
            var controller = new ReviewsController(_context, logger);


            var expectedReview = new ReviewDetailDTO(1, new DateTime(2025, 11, 17), "mperez", "España", DriverTypes.Novato, new List<ReviewItemDTO>());
            expectedReview.ReviewItems.Add(new ReviewItemDTO(1, "FIAT", "FIAT", "Rojo", "Gasolina", 4, "Excelente"));

            // Act 
            var result = await controller.GetReview(1);

            //Assert
            //we check that the response type is OK and obtain the rental
            var okResult = Assert.IsType<OkObjectResult>(result);
            var reviewDTOActual = Assert.IsType<ReviewDetailDTO>(okResult.Value);
            var eq = expectedReview.Equals(reviewDTOActual);
            //we check that the expected and actual are the same
            Assert.Equal(expectedReview, reviewDTOActual);
        }
    }
}