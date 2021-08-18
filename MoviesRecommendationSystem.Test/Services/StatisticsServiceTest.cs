namespace MoviesRecommendationSystem.Test.Services
{
    using Xunit;
    
    using MoviesRecommendationSystem.Test.Mocks;
    using MoviesRecommendationSystem.Services.Statistics;
    using MoviesRecommendationSystem.Services.Statistics.Models;

    public class StatisticsServiceTest
    {
        [Fact]
        public void GetTotalShouldReturnCorrectModel()
        {
            // Arrange

            var data = DataMock.Instance;
            var statisticsService = new StatisticsService(data);

            // Act

            var result = statisticsService.GetTotals();

            // Assert

            Assert.IsType<StatisticsServiceModel>(result);
        }
    }
}
