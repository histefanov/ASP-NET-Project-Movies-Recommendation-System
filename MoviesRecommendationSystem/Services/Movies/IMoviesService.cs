namespace MoviesRecommendationSystem.Services.Movies
{
    using System.Collections.Generic;
    using MoviesRecommendationSystem.Models.Enums;
    using MoviesRecommendationSystem.Services.Movies.Models;
    using MoviesRecommendationSystem.Services.Movies.Models.Genres;

    public interface IMoviesService
    {
        MovieQueryServiceModel All(
            string selectedGenre,
            string searchTerm,
            MovieSorting sorting,
            int currentPage,
            int moviesPerPage);

        IEnumerable<GenreServiceModel> AllGenres();
    }
}
