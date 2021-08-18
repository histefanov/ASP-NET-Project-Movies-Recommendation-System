namespace MoviesRecommendationSystem.Test.Controllers.Api
{
    using Xunit;
    using MyTested.AspNetCore.Mvc;

    using MoviesRecommendationSystem.Controllers.Api;
    using MoviesRecommendationSystem.Models.Api.Movies;
    using MoviesRecommendationSystem.Services.Movies.Models;

    public class MovieControllerTests
    {
        [Fact]
        public void AllReturnApiWithCorrectModel()
            => MyController<MoviesApiController>
                .Instance()
                .Calling(c => c.All(
                    With.Any<AllMoviesApiRequestModel>()))
                .ShouldReturn()
                .ResultOfType<MovieQueryServiceModel>();
                
    }
}
