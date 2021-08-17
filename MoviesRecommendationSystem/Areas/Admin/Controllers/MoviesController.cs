namespace MoviesRecommendationSystem.Areas.Admin.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using MoviesRecommendationSystem.Services.Editors;
    using MoviesRecommendationSystem.Services.Movies;

    public class MoviesController : AdminController
    {
        private readonly IMovieService movieService;

        public MoviesController(IMovieService movieService)
            => this.movieService = movieService;

        public IActionResult All() => View(this.movieService.All(publicOnly: false).Movies);

        public IActionResult SwitchVisibility(int id)
        {
            this.movieService.SwitchVisibility(id);

            return RedirectToAction(nameof(All), new { publicOnly = false });
        }
    }
}
