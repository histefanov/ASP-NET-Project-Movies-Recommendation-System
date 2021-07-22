namespace MoviesRecommendationSystem.Controllers.Api
{
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.AspNetCore.Mvc;
    using MoviesRecommendationSystem.Data;
    using MoviesRecommendationSystem.Data.Models;
    using MoviesRecommendationSystem.Models.Api.Movies;
    using MoviesRecommendationSystem.Models.Enums;
    using MoviesRecommendationSystem.Models.Movies;

    [ApiController]
    [Route("api/movies")]
    public class MoviesApiController : ControllerBase
    {
        private readonly MoviesRecommendationDbContext data;

        public MoviesApiController(MoviesRecommendationDbContext data) 
            => this.data = data;

        [HttpGet]
        public ActionResult<AllMoviesApiResponseModel> All([FromQuery]AllMoviesApiRequestModel query)
        {
            var moviesQuery = this.data.Movies.AsQueryable();

            if (!string.IsNullOrWhiteSpace(query.SelectedGenre))
            {
                var genreId = this.data
                    .Genres
                    .FirstOrDefault(g => g.Name == query.SelectedGenre)
                    .Id;

                moviesQuery = moviesQuery
                    .Where(m => m.MovieGenres.Any(mg => mg.GenreId == genreId));
            }

            moviesQuery = query.Sorting switch
            {
                MovieSorting.DateCreatedDescending => moviesQuery.OrderByDescending(m => m.ReleaseYear),
                MovieSorting.DateCreatedAscending => moviesQuery.OrderBy(m => m.ReleaseYear),
                MovieSorting.DateCreated or _ => moviesQuery.OrderByDescending(m => m.Id),
            };

            if (!string.IsNullOrWhiteSpace(query.SearchTerm))
            {
                //TODO: Improve code quality by replacing .ToLower() with another functionality
                moviesQuery = moviesQuery.Where(m =>
                    m.Title.ToLower().Contains(query.SearchTerm.ToLower()));
            }

            var totalMoviesCount = moviesQuery.Count();

            var movies = moviesQuery
                .Skip((query.CurrentPage - 1) * query.MoviesPerPage)
                .Take(query.MoviesPerPage)
                .Select(m => new MovieResponseModel
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

            return new AllMoviesApiResponseModel()
            {
                CurrentPage = query.CurrentPage,
                MoviesPerPage = query.MoviesPerPage,
                TotalMoviesCount = query.TotalMoviesCount,
                Movies = movies
            };
        }

        private IEnumerable<string> GetGenresAsStrings()
            => this.data
                .Genres
                .Select(g => g.Name)
                .OrderBy(n => n)
                .ToList();

        [HttpGet]
        [Route("{id}")]
        public IActionResult GetMovieDetails(int id)
        {
            var movie = this.data.Movies.Find(id);

            if (movie == null)
            {
                return NotFound();
            }

            return Ok(movie);
        }
    }
}
