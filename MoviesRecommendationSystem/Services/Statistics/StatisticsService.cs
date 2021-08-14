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
                TotalMovies = this.data.Movies.Count(m => m.IsPublic && !m.IsDeleted),
                TotalDirectors = this.data.Directors.Count(d => d.Movies.Any(m => m.IsPublic && !m.IsDeleted)),
                TotalActors = this.data.Actors.Count(a => a.MovieActors.Select(ma => ma.Movie).Any(m => m.IsPublic && !m.IsDeleted)),
                TotalGenres = this.data.MovieGenres
                    .Where(mg => mg.Movie.IsPublic && !mg.Movie.IsDeleted)
                    .Select(mg => mg.GenreId)
                    .Distinct()
                    .Count()
            };
        }
    }
}
