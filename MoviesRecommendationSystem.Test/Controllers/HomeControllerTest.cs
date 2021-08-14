namespace MoviesRecommendationSystem.Test.Controllers
{
    using MoviesRecommendationSystem.Controllers;
    using Xunit;
    using MyTested.AspNetCore.Mvc;

    public class HomeControllerTest
    {
        [Fact]
        public void IndexShouldReturnCorrectView()
            => MyController<HomeController>
                .Instance()
                .Calling(c => c.Index())
                .ShouldReturn()
                .View();

        [Fact]
        public void AboutShouldReturnCorrectView()
            => MyController<HomeController>
                .Instance()
                .Calling(c => c.About())
                .ShouldReturn()
                .View();

        [Fact]
        public void ErrorShouldReturnCorrectView()
            => MyController<HomeController>
                .Instance()
                .Calling(c => c.Error())
                .ShouldReturn()
                .View();
    }
}
