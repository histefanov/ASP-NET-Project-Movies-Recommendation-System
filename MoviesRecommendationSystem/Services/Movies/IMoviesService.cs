namespace MoviesRecommendationSystem.Services.Movies
{
    using System.Collections.Generic;
    using MoviesRecommendationSystem.Models.Enums;
    using MoviesRecommendationSystem.Models.Movies;
    using MoviesRecommendationSystem.Services.Movies.Models;

    public interface IMoviesService
    {
        MovieQueryServiceModel All(
            string selectedGenre,
            string searchTerm,
            MovieSorting sorting,
            int currentPage,
            int moviesPerPage);

        IEnumerable<MovieServiceModel> ByUser(string userId);

        IEnumerable<MovieGenreServiceModel> AllGenres();
    }
}
