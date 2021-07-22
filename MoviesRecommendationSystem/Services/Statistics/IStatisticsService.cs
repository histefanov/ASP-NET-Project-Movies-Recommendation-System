namespace MoviesRecommendationSystem.Services.Statistics
{
    using MoviesRecommendationSystem.Services.Statistics.Models;

    public interface IStatisticsService
    {
        StatisticsServiceModel GetTotals();
    }
}
