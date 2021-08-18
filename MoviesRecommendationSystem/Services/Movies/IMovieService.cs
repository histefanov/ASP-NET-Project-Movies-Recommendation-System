namespace MoviesRecommendationSystem.Services.Movies
{
    using System.Collections.Generic;

    using MoviesRecommendationSystem.Models.Enums;
    using MoviesRecommendationSystem.Services.Movies.Models;

    public interface IMovieService
    {
        int Create(
            string title,
            int releaseYear,
            int runTime,
            string plot,
            string language,
            string imageUrl,
            string playbackUrl,
            string youtubeTrailerId,
            string imdbId,
            string director,
            string studio,
            string actors,
            IEnumerable<string> genres,
            int editorId,
            bool IsPublic);

        bool Edit(
            int movieId,
            string title,
            int releaseYear,
            int runTime,
            string plot,
            string language,
            string imageUrl,
            string playbackUrl,
            string youtubeTrailerId,
            string ImdbId,
            string director,
            string studio,
            string actors,
            IEnumerable<string> genres,
            bool IsPublic);

        bool Delete(int movieId);

        MovieQueryServiceModel All(
            string selectedGenre = null,
            string searchTerm = null,
            MovieSorting sorting = MovieSorting.DateCreated,
            int currentPage = 1,
            int moviesPerPage = int.MaxValue,
            bool publicOnly = true);

        IEnumerable<MovieServiceModel> LastFourAddedMovies();

        MovieDetailsServiceModel Details(int id);

        MovieFormDataServiceModel FormDetails(int id);

        RandomMovieServiceModel Random();

        string GetRouteInfo(int movieId);

        void SwitchVisibility(int id);

        IEnumerable<MovieServiceModel> ByUser(string userId);

        bool IsByEditor(int movieId, int editorId);

        IEnumerable<MovieGenreServiceModel> AllGenres();

        IEnumerable<string> SelectedGenreIds(int movieId);

        bool GenreExists(int id);
    }
}
