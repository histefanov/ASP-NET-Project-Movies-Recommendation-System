namespace MoviesRecommendationSystem.Test.Pipeline
{
    using MoviesRecommendationSystem.Controllers;
    using Xunit;
    using MyTested.AspNetCore.Mvc;
    
    public class HomeControllerTest
    {
        [Fact]
        public void IndexShouldReturnView()
            => MyMvc
                .Pipeline()
                .ShouldMap("/")
                .To<HomeController>(c => c.Index())
                .Which()
                .ShouldReturn()
                .View();

        [Fact]
        public void AboutShouldReturnView()
            => MyMvc
                .Pipeline()
                .ShouldMap("/Home/About")
                .To<HomeController>(c => c.About())
                .Which()
                .ShouldReturn()
                .View();

        [Fact]
        public void ErrorShouldReturnView()
            => MyMvc
                .Pipeline()
                .ShouldMap("Home/Error")
                .To<HomeController>(c => c.Error())
                .Which()
                .ShouldReturn()
                .View();
    }
}
