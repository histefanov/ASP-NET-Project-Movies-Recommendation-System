namespace MoviesRecommendationSystem.Models.Movies
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using MoviesRecommendationSystem.Models.Enums;
    using MoviesRecommendationSystem.Services.Movies.Models;

    public class AllMoviesQueryModel
    {
        public const int MoviesPerPage = 4;

        public int TotalMoviesCount { get; set; }

        public string SelectedGenre { get; init; }

        [Display(Name = "Search")]
        public string SearchTerm { get; init; }

        public MovieSorting Sorting { get; init; }

        public int CurrentPage { get; init; } = 1;

        public IEnumerable<string> Genres { get; set; }

        public IEnumerable<MovieServiceModel> Movies { get; set; }
    }
}
