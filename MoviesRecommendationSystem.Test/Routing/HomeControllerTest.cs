namespace MoviesRecommendationSystem.Test.Routing
{
    using Xunit;
    using MyTested.AspNetCore.Mvc;
    
    using MoviesRecommendationSystem.Controllers;

    public class HomeControllerTest
    {
        [Fact]
        public void IndexRouteShouldBeMappedAccurately()
            => MyRouting
                .Configuration()
                .ShouldMap("/Home/Index")
                .To<HomeController>(c => c.Index());

        [Fact]
        public void RandomMovieShouldBeMappedAccurately()
            => MyRouting
                .Configuration()
                .ShouldMap("/Home/RandomMovie")
                .To<HomeController>(c => c.RandomMovie());

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
