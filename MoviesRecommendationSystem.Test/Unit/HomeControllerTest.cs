namespace MoviesRecommendationSystem.Test.Unit
{
    using Microsoft.AspNetCore.Mvc;
    using MoviesRecommendationSystem.Controllers;
    using MoviesRecommendationSystem.Test.Mocks;
    using Xunit;

    public class HomeControllerTest
    {
        [Fact]
        public void ErrorActionShouldReturnView()
        {
            //Arrange
            var homeController = new HomeController(null, null, null);

            //Act
            var result = homeController.Error();

            //Assert
            Assert.NotNull(result);
            Assert.IsType<ViewResult>(result);
        }
    }
}
