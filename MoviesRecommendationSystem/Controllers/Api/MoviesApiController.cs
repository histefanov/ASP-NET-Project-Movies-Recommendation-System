namespace MoviesRecommendationSystem.Controllers.Api
{
    using Microsoft.AspNetCore.Mvc;
    using MoviesRecommendationSystem.Models.Api.Movies;
    using MoviesRecommendationSystem.Services.Movies;
    using MoviesRecommendationSystem.Services.Movies.Models;

    [ApiController]
    [Route("api/movies")]
    public class MoviesApiController : ControllerBase
    {
        private readonly IMovieService movies;

        public MoviesApiController(IMovieService movies) 
            => this.movies = movies;

        [HttpGet]
        public MovieQueryServiceModel All([FromQuery] AllMoviesApiRequestModel query) 
            => this.movies.All(
                query.SelectedGenre,
                query.SearchTerm,
                query.Sorting,
                query.CurrentPage,
                query.MoviesPerPage);
    }
}
