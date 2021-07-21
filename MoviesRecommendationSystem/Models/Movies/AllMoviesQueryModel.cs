namespace MoviesRecommendationSystem.Models.Movies
{
    using MoviesRecommendationSystem.Models.Movies.Enums;
    using System.Collections.Generic;

    public class AllMoviesQueryModel
    {
        public IEnumerable<string> Genres { get; init; }

        public IEnumerable<string> SearchTerm { get; init; }

        public MovieSorting Sorting { get; init; }

        public IEnumerable<MovieListingViewModel> Movies { get; init; }
    }
}
