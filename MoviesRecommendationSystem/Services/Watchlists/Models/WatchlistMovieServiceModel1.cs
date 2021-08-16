namespace MoviesRecommendationSystem.Services.Watchlists.Models
{
    using MoviesRecommendationSystem.Services.Movies.Models.Interfaces;

    public class WatchlistMovieServiceModel : IMovieServiceModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public int? ReleaseYear { get; set; }
    }
}