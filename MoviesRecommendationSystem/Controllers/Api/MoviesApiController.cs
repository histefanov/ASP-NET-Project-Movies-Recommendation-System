namespace MoviesRecommendationSystem.Controllers.Api
{
    using Microsoft.AspNetCore.Mvc;

    using MoviesRecommendationSystem.Models.Api.Movies;
    using MoviesRecommendationSystem.Services.Movies;
    using MoviesRecommendationSystem.Services.Movies.Models;

    [ApiController]
    [Route("/api/movies")]
    public class MoviesApiController : ControllerBase
    {
        private readonly IMovieService movieService;

        public MoviesApiController(IMovieService movieService) 
            => this.movieService = movieService;

        public MovieQueryServiceModel All([FromQuery] AllMoviesApiRequestModel query) 
            => this.movieService.All();
    }
}
