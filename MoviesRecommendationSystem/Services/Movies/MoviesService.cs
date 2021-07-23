namespace MoviesRecommendationSystem.Services.Movies
{
    using System.Collections.Generic;
    using System.Linq;
    using MoviesRecommendationSystem.Data;
    using MoviesRecommendationSystem.Models.Enums;
    using MoviesRecommendationSystem.Services.Movies.Models;
    using MoviesRecommendationSystem.Services.Movies.Models.Genres;

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

            var movies = moviesQuery
                .Skip((currentPage - 1) * moviesPerPage)
                .Take(moviesPerPage)
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

            return new MovieQueryServiceModel
            {
                TotalMovies = totalMoviesCount,
                CurrentPage = currentPage,
                MoviesPerPage = moviesPerPage,
                Movies = movies
            };
        }

        public IEnumerable<GenreServiceModel> AllGenres()
            => this.data.Genres
                   .Select(g => new GenreServiceModel
                   {
                       Id = g.Id,
                       Name = g.Name
                   })
                   .OrderBy(g => g.Name)
                   .ToList();
    }
}
