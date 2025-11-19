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
    public class PostReviews_test : AppForSEII25264SqliteUT
    {
        private const string _username = "mperez";
        private const string _country = "España";
        private const string _model1 = "FIAT";
        private const string _model2 = "KIA";
        private const string _model3 = "SEAT";

        public PostReviews_test()
        {
            var models = new List<Model>()
            {
                new Model(_model1),
                new Model(_model2),
                new Model(_model3)
            };

            var cars = new List<Car>(){
                new Car(models[0], "Fiat", "FIAT", "Gasolina", "Rojo"),
                new Car(models[1], "Kia", "KIA", "Gasoil", "Negro"),
                new Car(models[2], "Seat", "SEAT", "Diesel", "Azul")
            };

            ApplicationUser user = new ApplicationUser(_username);

            var review = new Review(user, _country, new DateTime(2025, 11, 17), DriverTypes.Novato, new List<ReviewItem>());
            review.ReviewItems.Add(new ReviewItem(review, cars[0], 4, "Excelente"));

            _context.ApplicationUsers.Add(user);
            _context.AddRange(models);
            _context.AddRange(cars);
            _context.Add(review);
            _context.SaveChanges();
        }

        public static IEnumerable<object[]> TestCasesFor_CreateReview()
        {
            var reviewNoItem = new ReviewForCreateDTO(_username, _country, DriverTypes.Novato, new List<ReviewItemDTO>());

            var reviewItems = new List<ReviewItemDTO>() { new ReviewItemDTO(2, _model1, "FIAT", "Rojo", 4, "Excelente") };

            var RentalApplicationUser = new ReviewForCreateDTO("angel", _country, DriverTypes.Novato, reviewItems);

            var allTests = new List<object[]>
            {             //input for createpurchase - Error expected
                new object[] { reviewNoItem, "Error! You must include at least one car to be reviewed",  },
                new object[] { RentalApplicationUser, "Error! That user is not registered", },
            };

            return allTests;
        }

        [Theory]
        [Trait("LevelTesting", "Unit Testing")]
        [Trait("Database", "WithoutFixture")]
        [MemberData(nameof(TestCasesFor_CreateReview))]
        public async Task CreateReview_Error_test(ReviewForCreateDTO reviewDTO, string errorExpected)
        {
            // Arrange
            var mock = new Mock<ILogger<ReviewsController>>();
            ILogger<ReviewsController> logger = mock.Object;

            var controller = new ReviewsController(_context, logger);

            // Act
            var result = await controller.CreateReview(reviewDTO);

            //Assert
            //we check that the response type is BadRequest and obtain the error returned
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            var problemDetails = Assert.IsType<ValidationProblemDetails>(badRequestResult.Value);

            var errorActual = problemDetails.Errors.First().Value[0];

            //we check that the expected error message and actual are the same
            Assert.StartsWith(errorExpected, errorActual);

        }

        [Fact]
        [Trait("LevelTesting", "Unit Testing")]
        [Trait("Database", "WithoutFixture")]
        public async Task CreateReview_Success_test()
        {
            // Arrange
            var mock = new Mock<ILogger<ReviewsController>>();
            ILogger<ReviewsController> logger = mock.Object;

            var controller = new ReviewsController(_context, logger);

            var reviewDTO = new ReviewForCreateDTO(_username, _country, DriverTypes.Novato, 
                new List<ReviewItemDTO>() { new ReviewItemDTO(1, _model1, "FIAT", "Rojo", 4, "Excelente")});

            var expectedreviewDetailDTO = new ReviewDetailDTO(2, DateTime.Today, _username, _country, DriverTypes.Novato,
                new List<ReviewItemDTO>() { new ReviewItemDTO(1, _model1, "FIAT", "Rojo", 4, "Excelente") });

            // Act
            var result = await controller.CreateReview(reviewDTO);

            //Assert
            //we check that the response type is BadRequest and obtain the error returned
            var createdResult = Assert.IsType<CreatedAtActionResult>(result);
            var actualReviewDetailDTO = Assert.IsType<ReviewDetailDTO>(createdResult.Value);

            Assert.Equal(expectedreviewDetailDTO, actualReviewDetailDTO);

        }
    }
}
