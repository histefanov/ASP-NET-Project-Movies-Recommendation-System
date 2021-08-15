namespace MoviesRecommendationSystem.Models.Home
{
    using MoviesRecommendationSystem.Services.Movies.Models;
    using System.Collections.Generic;

    public class IndexViewModel
    {
        public int TotalMovies { get; init; }

        public int TotalGenres { get; init; }

        public int TotalActors { get; init; }

        public int TotalDirectors { get; init; }

        public IEnumerable<MovieServiceModel> RecentlyAddedMovies { get; init; }
    }
}
