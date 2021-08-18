namespace MoviesRecommendationSystem.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Caching.Memory;

    using MoviesRecommendationSystem.Models.Home;
    using MoviesRecommendationSystem.Services.Movies;
    using MoviesRecommendationSystem.Services.Movies.Models;
    using MoviesRecommendationSystem.Services.Statistics;

    using static WebConstants;

    public class HomeController : Controller
    {
        private readonly IStatisticsService statisticsService;
        private readonly IMovieService movieService;
        private readonly IMemoryCache cache;

        public HomeController(
            IStatisticsService statisticsService, 
            IMovieService movieService,
            IMemoryCache cache)
        {
            this.statisticsService = statisticsService;
            this.movieService = movieService;
            this.cache = cache;
        }

        public IActionResult Index()
        {
            var recentlyAddedMovies = this.cache
                .Get<List<MovieServiceModel>>(RecentlyAddedMoviesCacheKey);

            if (recentlyAddedMovies == null)
            {
                recentlyAddedMovies = movieService
                    .LastFourAddedMovies()
                    .ToList();

                var cacheOptions = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromMinutes(60))
                    .SetPriority(CacheItemPriority.High);

                this.cache.Set(RecentlyAddedMoviesCacheKey, recentlyAddedMovies, cacheOptions);
            }

            var statisticsTotals = this.statisticsService.GetTotals();

            return View(new IndexViewModel
            {
                TotalMovies = statisticsTotals.TotalMovies,
                TotalDirectors = statisticsTotals.TotalDirectors,
                TotalActors = statisticsTotals.TotalActors,
                TotalGenres = statisticsTotals.TotalGenres,
                RecentlyAddedMovies = recentlyAddedMovies
            });
        }

        public IActionResult RandomMovie()
        {
            var movie = this.movieService.Random();

            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

        public IActionResult About()
            => View();

        public IActionResult Error()
            => View();
    }
}
