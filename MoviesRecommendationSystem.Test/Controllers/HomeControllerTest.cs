namespace MoviesRecommendationSystem.Test.Controllers
{
    using Xunit;
    using MyTested.AspNetCore.Mvc;
    using Data;
    
    using MoviesRecommendationSystem.Controllers;
    using MoviesRecommendationSystem.Services.Movies.Models;

    public class HomeControllerTest
    {
        [Fact]
        public void IndexShouldReturnCorrectView()
            => MyController<HomeController>
                .Instance()
                .Calling(c => c.Index())
                .ShouldReturn()
                .View();

        [Theory]
        [InlineData("Title", 2010)]
        public void RandomMovieShouldReturnCorrectView(string title, int releaseYear)
            => MyController<HomeController>
                .Instance(controller => controller
                    .WithData(data => data
                        .WithEntities(
                            Data.GetDirector(),
                            Data.GetActor(),
                            Data.GetGenre(),
                            Data.GetPublicMovie(),
                            Data.GetMovieActor(),
                            Data.GetMovieGenre())))                            
                .Calling(c => c.RandomMovie())
                .ShouldReturn()
                .View(view => view
                    .WithModelOfType<RandomMovieServiceModel>()
                    .Passing(data =>
                    {
                        Assert.NotNull(data);
                        Assert.Equal(title, data.Title);
                        Assert.Equal(releaseYear, data.ReleaseYear);
                    }));

        [Fact]
        public void RandomMovieShouldReturnNotFoundWhenNoMoviesAreAvailable()
            => MyController<HomeController>
                .Instance()
                .Calling(c => c.RandomMovie())
                .ShouldReturn()
                .NotFound();


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
