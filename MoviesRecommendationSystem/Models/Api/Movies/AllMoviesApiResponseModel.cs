namespace MoviesRecommendationSystem.Models.Api.Movies
{
    using System.Collections.Generic;

    public class AllMoviesApiResponseModel
    {
        public int CurrentPage { get; init; } = 1;

        public int TotalMoviesCount { get; init; }

        public int MoviesPerPage { get; init; }

        public IEnumerable<MovieResponseModel> Movies { get; init; }
    }
}
