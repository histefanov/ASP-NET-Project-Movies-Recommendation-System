namespace MoviesRecommendationSystem.Test.Routing.Api
{
    using Xunit;
    using MyTested.AspNetCore.Mvc;

    using MoviesRecommendationSystem.Controllers.Api;
    using MoviesRecommendationSystem.Models.Api.Movies;

    public class MoviesApiControllerTest
    {
        [Fact]
        public void AllShouldBeMappedAccurately()
            => MyRouting
                .Configuration()
                .ShouldMap("/api/movies")
                .To<MoviesApiController>(c => c.All(
                    With.Any<AllMoviesApiRequestModel>()));
    }
}
