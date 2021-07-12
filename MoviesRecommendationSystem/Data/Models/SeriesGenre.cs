namespace MoviesRecommendationSystem.Data.Models
{
    public class SeriesGenre
    {
        public int SeriesId { get; set; }

        public Series Series { get; set; }

        public int GenreId { get; set; }

        public Genre Genre { get; set; }
    }
}
