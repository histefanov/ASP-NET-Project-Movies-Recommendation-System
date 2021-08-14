namespace MoviesRecommendationSystem.Test.Routing
{
    using MoviesRecommendationSystem.Controllers;
    using Xunit;
    using MyTested.AspNetCore.Mvc;

    public class HomeControllerTest
    {
        [Fact]
        public void IndexRouteShouldBeMappedAccurately()
            => MyRouting
                .Configuration()
                .ShouldMap("/Home/Index")
                .To<HomeController>(c => c.Index());

        [Fact]
        public void AboutRouteShouldBeMappedAccurately()
            => MyRouting
                .Configuration()
                .ShouldMap("/Home/About")
                .To<HomeController>(c => c.About());

        [Fact]
        public void ErrorRouteShouldBeMappedAccurately()
            => MyRouting
                .Configuration()
                .ShouldMap("/Home/Error")
                .To<HomeController>(c => c.Error());
    }
}
