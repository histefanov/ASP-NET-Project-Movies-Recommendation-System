namespace MoviesRecommendationSystem.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using MoviesRecommendationSystem.Infrastructure;
    using MoviesRecommendationSystem.Services.Watchlists;

    using static Common.ControllerConstants.Watchlists;

    public class WatchlistsController : Controller
    {
        private readonly IWatchlistService watchlistService;

        public WatchlistsController(IWatchlistService watchlistService)
            => this.watchlistService = watchlistService;

        [Authorize]
        [Route(AddRoute)]
        public IActionResult Add(int movieId)
        {
            var userId = User.GetId();

            var isSuccess = this.watchlistService.Add(userId, movieId);

            if (!isSuccess)
            {
                return BadRequest();
            }

            return RedirectToAction(nameof(MoviesController.Details), MoviesControllerName, new { id = movieId });
        }

        [Authorize]
        [Route(RemoveRoute)]
        public IActionResult Remove(int movieId)
        {
            var userId = User.GetId();

            var isSuccess = this.watchlistService.Remove(userId, movieId);

            if (!isSuccess)
            {
                return BadRequest();
            }

            return RedirectToAction(nameof(MoviesController.Details), MoviesControllerName, new { id = movieId });
        }

        [Authorize]
        public IActionResult All(string userId)
        {
            if (!User.Identity.IsAuthenticated || User.GetId() != userId)
            {
                return BadRequest();
            }

            var movies = this.watchlistService.GetMoviesDetailedByUser(userId);

            return View(movies);
        }
    }
}
