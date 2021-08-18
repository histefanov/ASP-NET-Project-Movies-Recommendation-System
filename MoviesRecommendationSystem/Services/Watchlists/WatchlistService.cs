namespace MoviesRecommendationSystem.Services.Watchlists
{
    using System.Linq;
    using System.Collections.Generic;
    using AutoMapper;
    using AutoMapper.QueryableExtensions;

    using MoviesRecommendationSystem.Data;
    using MoviesRecommendationSystem.Data.Models;
    using MoviesRecommendationSystem.Services.Watchlists.Models;
    using MoviesRecommendationSystem.Services.Movies.Models;

    public class WatchlistService : IWatchlistService
    {
        private readonly MoviesRecommendationDbContext data;
        private readonly IMapper mapper;

        public WatchlistService(MoviesRecommendationDbContext data, IMapper mapper)
        {
            this.data = data;
            this.mapper = mapper;
        }

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
            var userWatchlistMovie = this.data
                .UserWatchlistMovies
                .FirstOrDefault(w => w.UserId == userId && w.MovieId == movieId);

            if (userWatchlistMovie == null)
            {
                return false;
            }

            this.data
                .UserWatchlistMovies
                .Remove(userWatchlistMovie);

            this.data.SaveChanges();

            return true;
        }

        public void RemoveForAllUsers(int movieId)
        {
            var movies = this.data
                .UserWatchlistMovies
                .Where(w => w.MovieId == movieId)
                .ToList();

            foreach (var movie in movies)
            {
                this.data
                    .UserWatchlistMovies
                    .Remove(movie);
            }

            this.data.SaveChanges();
        }

        public int Count(string userid)
            => this.data
                .UserWatchlistMovies
                .Count(w => w.UserId == userid);

        public bool Exists(string userid, int movieId)
            => this.data
                .UserWatchlistMovies
                .Any(w => w.UserId == userid && w.MovieId == movieId);

        public IEnumerable<WatchlistMovieServiceModel> GetMoviesByUser(string userId)
            => this.data
                .UserWatchlistMovies
                .Where(w => w.UserId == userId)
                .OrderByDescending(w => w.DateCreated)
                .ProjectTo<WatchlistMovieServiceModel>(this.mapper.ConfigurationProvider)
                .ToList();

        public IEnumerable<MovieServiceModel> GetMoviesDetailedByUser(string userId)
            => this.data
                .UserWatchlistMovies
                .Where(w => w.UserId == userId)
                .OrderByDescending(w => w.DateCreated)
                .Select(w => w.Movie)
                .ProjectTo<MovieServiceModel>(this.mapper.ConfigurationProvider)
                .ToList();
    }
}
