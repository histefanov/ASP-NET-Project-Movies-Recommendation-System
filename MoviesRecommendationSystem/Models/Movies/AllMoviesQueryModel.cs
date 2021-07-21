namespace MoviesRecommendationSystem.Models.Movies
{
    using MoviesRecommendationSystem.Models.Movies.Enums;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class AllMoviesQueryModel
    {
        public IEnumerable<string> Genres { get; init; }

        [Display(Name = "Search")]
        public IEnumerable<string> SearchTerm { get; init; }

        public MovieSorting Sorting { get; init; }

        public IEnumerable<MovieListingViewModel> Movies { get; init; }
    }
}
