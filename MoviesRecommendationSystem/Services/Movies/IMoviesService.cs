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

        bool Edit(
            int movieId,
            string title,
            int releaseYear,
            int runTime,
            string plot,
            string language,
            string imageUrl,
            string director,
            string studio,
            string actors,
            IEnumerable<string> genres);

        MovieQueryServiceModel All(
            string selectedGenre,
            string searchTerm,
            MovieSorting sorting,
            int currentPage,
            int moviesPerPage);

        MovieDetailsServiceModel Details(int id);

        IEnumerable<MovieServiceModel> ByUser(string userId);

        bool IsByEditor(int movieId, int editorId);

        IEnumerable<MovieGenreServiceModel> AllGenres();

        IEnumerable<string> SelectedGenreIds(int movieId);

        bool GenreExists(int id);
    }
}
