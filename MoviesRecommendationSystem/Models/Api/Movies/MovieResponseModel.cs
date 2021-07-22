namespace MoviesRecommendationSystem.Models.Api.Movies
{
    using System.Collections.Generic;

    public class MovieResponseModel
    {
        public int Id { get; init; }

        public string Title { get; init; }

        public int ReleaseYear { get; init; }

        public string Plot { get; init; }

        public string ImageUrl { get; init; }

        public IList<string> Genres { get; init; }
    }
}
