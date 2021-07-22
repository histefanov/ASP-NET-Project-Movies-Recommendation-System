namespace MoviesRecommendationSystem.Services.Movies
{
    using MoviesRecommendationSystem.Services.Movies.Models;

    public interface IMoviesService
    {
        MovieQueryServiceModel All();
    }
}
