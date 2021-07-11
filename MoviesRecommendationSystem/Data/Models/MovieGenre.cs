namespace MoviesRecommendationSystem.Data.Models
{
    public class MovieGenre
    {
        public int MovieId { get; init; }

        public Movie Movie { get; set; }

        public int GenreId { get; init; }

        public Genre Genre { get; set; }
    }
}
