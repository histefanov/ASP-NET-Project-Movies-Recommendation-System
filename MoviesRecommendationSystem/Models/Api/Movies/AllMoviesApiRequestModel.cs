namespace MoviesRecommendationSystem.Models.Api.Movies
{
    using MoviesRecommendationSystem.Models.Enums;

    public class AllMoviesApiRequestModel
    {
        public int TotalMoviesCount { get; set; }

        public string SelectedGenre { get; init; }

        public string SearchTerm { get; init; }

        public MovieSorting Sorting { get; init; }

        public int MoviesPerPage { get; init; } = 10;

        public int CurrentPage { get; init; } = 1;
    }
}
