namespace MoviesRecommendationSystem.Services.Movies
{
    using System.Collections.Generic;
    using MoviesRecommendationSystem.Models.Enums;
    using MoviesRecommendationSystem.Services.Movies.Models;

    public interface IMoviesService
    {
        int Create(
            string title,
            int releaseYear,
            int runTime,
            string plot,
            string language,
            string imageUrl,
            string director,
            string studio,
            string actors,
            IEnumerable<string> genres,
            int editorId);

        MovieQueryServiceModel All(
            string selectedGenre,
            string searchTerm,
            MovieSorting sorting,
            int currentPage,
            int moviesPerPage);

        MovieServiceModel Details(int id);

        IEnumerable<MovieServiceModel> ByUser(string userId);

        IEnumerable<MovieGenreServiceModel> AllGenres();

        bool GenreExists(int id);
    }
}
