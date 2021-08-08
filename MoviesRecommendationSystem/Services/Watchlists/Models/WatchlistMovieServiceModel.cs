namespace MoviesRecommendationSystem.Services.Watchlists.Models
{
    public class WatchlistMovieServiceModel
    {
        public string UserId { get; init; }

        public int MovieId { get; init; }

        public string MovieTitle { get; init; }

        public int MovieReleaseYear { get; set; }
    }
}
