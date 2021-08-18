namespace MoviesRecommendationSystem.Controllers.Api
{
    using Microsoft.AspNetCore.Mvc;

    using MoviesRecommendationSystem.Services.Statistics;
    using MoviesRecommendationSystem.Services.Statistics.Models;

    [ApiController]
    [Route("/api/statistics")]
    public class StatisticsApiController : ControllerBase
    {
        private readonly IStatisticsService statistics;

        public StatisticsApiController(IStatisticsService statistics) 
            => this.statistics = statistics;

        public StatisticsServiceModel GetStatistics() 
            => this.statistics.GetTotals();
    }
}
