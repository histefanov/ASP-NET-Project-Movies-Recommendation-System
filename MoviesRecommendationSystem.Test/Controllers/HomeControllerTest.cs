namespace MoviesRecommendationSystem.Test.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using MoviesRecommendationSystem.Controllers;
    using Xunit;

    
    public class HomeControllerTest
    {
        [Fact]
        public void ErrorShouldReturnView()
        {
            //Arrange
            var homeController = new HomeController(null);

            //Act
            var result = homeController.Error();

            //Assert
            Assert.NotNull(result);
            Assert.IsType<ViewResult>(result);
        }
    }
}
