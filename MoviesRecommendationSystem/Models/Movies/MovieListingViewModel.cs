namespace MoviesRecommendationSystem.Models.Movies
{
    public class MovieListingViewModel
    {
        public int Id { get; init; }

        public string Title { get; init; }

        public int ReleaseYear { get; init; }

        public string Plot { get; init; }

        public string ImageUrl { get; init; }

        // IEnumerable<Genres> ...
    }
}
