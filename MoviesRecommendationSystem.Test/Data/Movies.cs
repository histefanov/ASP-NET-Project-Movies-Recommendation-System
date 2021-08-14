namespace MoviesRecommendationSystem.Test.Data
{
    using MoviesRecommendationSystem.Data.Models;
    using System.Collections.Generic;
    using System.Linq;

    public static class Movies
    {
        public static IEnumerable<Movie> GetTenPublicMovies()
            => Enumerable.Range(0, 10).Select(x => new Movie
            {
                IsPublic = true
            });
    }
}
