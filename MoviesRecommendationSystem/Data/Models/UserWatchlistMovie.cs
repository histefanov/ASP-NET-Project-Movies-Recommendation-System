namespace MoviesRecommendationSystem.Data.Models
{
    public class UserWatchlistMovie
    {
        public string UserId { get; init; }

        public User User { get; set; }

        public int MovieId { get; init; }

        public Movie Movie { get; set; }
    }
}
