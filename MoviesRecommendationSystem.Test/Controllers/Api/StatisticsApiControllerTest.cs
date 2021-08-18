namespace MoviesRecommendationSystem.Test.Controllers.Api
{
    using Xunit;
    using MyTested.AspNetCore.Mvc;

    using MoviesRecommendationSystem.Controllers.Api;
    using MoviesRecommendationSystem.Services.Statistics.Models;

    public class StatisticsApiControllerTests
    {
        [Fact]
        public void GetStatisticsReturnApiWithCorrectModel()
            => MyController<StatisticsApiController>
                .Instance()
                .Calling(c => c.GetStatistics())
                .ShouldReturn()
                .ResultOfType<StatisticsServiceModel>();

    }
}
