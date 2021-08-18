namespace MoviesRecommendationSystem.Test.Routing.Api
{
    using Xunit;
    using MyTested.AspNetCore.Mvc;

    using MoviesRecommendationSystem.Controllers.Api;

    public class StatisticsApiControllerTest
    {
        [Fact]
        public void GetStatisticsShouldBeMappedAccurately()
            => MyRouting
                .Configuration()
                .ShouldMap("/api/statistics")
                .To<StatisticsApiController>(c => c.GetStatistics());
    }
}
