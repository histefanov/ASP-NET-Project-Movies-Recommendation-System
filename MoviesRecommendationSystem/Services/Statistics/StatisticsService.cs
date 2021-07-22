namespace MoviesRecommendationSystem.Services.Statistics
{
    using System.Linq;
    using MoviesRecommendationSystem.Data;
    using MoviesRecommendationSystem.Services.Statistics.Models;

    public class StatisticsService : IStatisticsService
    {
        private readonly MoviesRecommendationDbContext data;

        public StatisticsService(MoviesRecommendationDbContext data) 
            => this.data = data;

        public StatisticsServiceModel GetTotals()
        {
            return new StatisticsServiceModel()
            {
                TotalMovies = this.data.Movies.Count(),
                TotalDirectors = this.data.Directors.Count(),
                TotalActors = this.data.Actors.Count(),
                TotalGenres = this.data.MovieGenres
                    .Select(mg => mg.GenreId)
                    .Distinct()
                    .Count()
            };
        }
    }
}
