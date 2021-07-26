namespace MoviesRecommendationSystem.Services.Movies
{
    using System.Collections.Generic;
    using System.Linq;
    using MoviesRecommendationSystem.Data;
    using MoviesRecommendationSystem.Data.Models;
    using MoviesRecommendationSystem.Models.Enums;
    using MoviesRecommendationSystem.Models.Movies;
    using MoviesRecommendationSystem.Services.Movies.Models;

    public class MoviesService : IMoviesService
    {
        private readonly MoviesRecommendationDbContext data;

        public MoviesService(MoviesRecommendationDbContext data) 
            => this.data = data;

        public MovieQueryServiceModel All(
            string selectedGenre,
            string searchTerm,
            MovieSorting sorting,
            int currentPage,
            int moviesPerPage)
        {
            var moviesQuery = this.data.Movies.AsQueryable();

            if (!string.IsNullOrWhiteSpace(selectedGenre))
            {
                var genreId = this.data
                    .Genres
                    .FirstOrDefault(g => g.Name == selectedGenre)
                    .Id;

                moviesQuery = moviesQuery
                    .Where(m => m.MovieGenres.Any(mg => mg.GenreId == genreId));
            }

            moviesQuery = sorting switch
            {
                MovieSorting.DateCreatedDescending => moviesQuery.OrderByDescending(m => m.ReleaseYear),
                MovieSorting.DateCreatedAscending => moviesQuery.OrderBy(m => m.ReleaseYear),
                MovieSorting.DateCreated or _ => moviesQuery.OrderByDescending(m => m.Id),
            };

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                //TODO: Improve code quality by replacing .ToLower() with another functionality
                moviesQuery = moviesQuery.Where(m =>
                    m.Title.ToLower().Contains(searchTerm.ToLower()));
            }

            var totalMoviesCount = moviesQuery.Count();

            var movies = GetMovies(
                 moviesQuery
                    .Skip((currentPage - 1) * moviesPerPage)
                    .Take(moviesPerPage));

            return new MovieQueryServiceModel
            {
                TotalMovies = totalMoviesCount,
                CurrentPage = currentPage,
                MoviesPerPage = moviesPerPage,
                Movies = movies
            };
        }

        public IEnumerable<MovieGenreServiceModel> AllGenres()
            => this.data.Genres
                   .Select(g => new MovieGenreServiceModel
                   {
                       Id = g.Id,
                       Name = g.Name
                   })
                   .OrderBy(g => g.Name)
                   .ToList();

        public IEnumerable<MovieServiceModel> ByUser(string userId)
            => this.GetMovies(this.data
                .Movies
                .Where(m => m.Editor.UserId == userId));

        private IEnumerable<MovieServiceModel> GetMovies(IQueryable<Movie> movieQuery)
            => movieQuery
                .Select(m => new MovieServiceModel
                {
                    Id = m.Id,
                    Title = m.Title,
                    ReleaseYear = m.ReleaseYear,
                    Plot = m.Plot,
                    ImageUrl = m.ImageUrl,
                    Genres = m.MovieGenres
                                .Select(mg => mg.Genre.Name)
                                .ToList()
                })
                .ToList();
    }
}
