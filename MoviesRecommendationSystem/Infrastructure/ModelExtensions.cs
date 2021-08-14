namespace MoviesRecommendationSystem.Infrastructure
{
    using MoviesRecommendationSystem.Services.Movies.Models.Interfaces;

    public static class ModelExtensions
    {
        public static string GetInfo(this IMovieServiceModel movie)
            => movie.Title + "-" + movie.ReleaseYear;
    }
}
