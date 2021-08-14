namespace MoviesRecommendationSystem.Services.Movies.Models
{
    using MoviesRecommendationSystem.Services.Movies.Models.Interfaces;

    public class MovieRandomServiceModel : IMovieServiceModel
    {
        public int Id { get; init; }

        public string Title { get; init; }

        public int ReleaseYear { get; init; }
    }
}
