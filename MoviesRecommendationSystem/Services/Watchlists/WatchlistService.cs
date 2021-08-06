namespace MoviesRecommendationSystem.Services.Watchlists
{
    using System.Linq;
    using MoviesRecommendationSystem.Data;
    using MoviesRecommendationSystem.Data.Models;

    public class WatchlistService : IWatchlistService
    {
        private readonly MoviesRecommendationDbContext data;

        public WatchlistService(MoviesRecommendationDbContext data)
            => this.data = data;

        public bool Add(string userId, int movieId)
        {
            if (this.data.UserWatchlistMovies.Any(w => w.UserId == userId && w.MovieId == movieId))
            {
                return false;
            }

            this.data.UserWatchlistMovies.Add(new UserWatchlistMovie
            {
                UserId = userId,
                MovieId = movieId
            });

            this.data.SaveChanges();

            return true;
        }

        public bool Remove(string userId, int movieId)
        {
            throw new System.NotImplementedException();
        }

        public int Count(string userid)
            => this.data
                .UserWatchlistMovies
                .Count(w => w.UserId == userid);

        public bool Exists(string userid, int movieId)
            => this.data
                .UserWatchlistMovies
                .Any(w => w.UserId == userid && w.MovieId == movieId);
    }
}
