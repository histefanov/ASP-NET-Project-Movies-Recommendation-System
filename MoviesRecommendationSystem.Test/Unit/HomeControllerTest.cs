namespace MoviesRecommendationSystem.Test.Unit
{
    using Xunit;
    using Microsoft.AspNetCore.Mvc;

    using MoviesRecommendationSystem.Controllers;

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
