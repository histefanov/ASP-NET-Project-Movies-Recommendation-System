namespace MoviesRecommendationSystem.Services.Watchlists
{
    using MoviesRecommendationSystem.Services.Watchlists.Models;

    public interface IWatchlistService
    {
        bool Add(string userId, int movieId);

        bool Remove(string userId, int movieId);

        int Count(string userid);

        bool Exists(string userId, int movieId);

        WatchlistMovieServiceModel GetMovie(string userId, int movieId); 
    }
}
