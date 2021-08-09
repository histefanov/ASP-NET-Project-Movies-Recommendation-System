namespace MoviesRecommendationSystem.Services.Watchlists
{
    using System.Collections.Generic;
    using MoviesRecommendationSystem.Services.Movies.Models;
    using MoviesRecommendationSystem.Services.Watchlists.Models;

    public interface IWatchlistService
    {
        bool Add(string userId, int movieId);

        bool Remove(string userId, int movieId);

        int Count(string userid);

        bool Exists(string userId, int movieId);

        IEnumerable<WatchlistMovieServiceModel> GetMoviesByUser(string userId);

        IEnumerable<MovieServiceModel> GetMoviesDetailedByUser(string userId);
    }
}
