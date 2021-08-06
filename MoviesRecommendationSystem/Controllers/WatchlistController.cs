﻿namespace MoviesRecommendationSystem.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using MoviesRecommendationSystem.Infrastructure;
    using MoviesRecommendationSystem.Services.Watchlists;

    public class WatchlistController : Controller
    {
        private readonly IWatchlistService watchlistService;

        public WatchlistController(IWatchlistService watchlistService)
            => this.watchlistService = watchlistService;

        [Authorize]
        [Route("Watchlist/Add/{movieId}")]
        public IActionResult Add(int movieId)
        {
            var userId = this.User.GetId();

            var isSuccess = this.watchlistService.Add(userId, movieId);

            if (!isSuccess)
            {
                return BadRequest();
            }

            return RedirectToAction("Details", "Movies", new { id = movieId });
        }
    }
}