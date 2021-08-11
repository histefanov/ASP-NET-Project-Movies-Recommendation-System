namespace MoviesRecommendationSystem.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using MoviesRecommendationSystem.Infrastructure;
    using MoviesRecommendationSystem.Models;
    using MoviesRecommendationSystem.Models.Home;
    using MoviesRecommendationSystem.Services.Statistics;
    using MoviesRecommendationSystem.Services.Watchlists;

    public class HomeController : Controller
    {
        private readonly IStatisticsService statisticsService;

        public HomeController(IStatisticsService statisticsService)
            => this.statisticsService = statisticsService;

        public IActionResult Index()
        {
            var statisticsTotals = this.statisticsService.GetTotals();

            return View(new IndexViewModel
            {
                TotalMovies = statisticsTotals.TotalMovies,
                TotalDirectors = statisticsTotals.TotalDirectors,
                TotalActors = statisticsTotals.TotalActors,
                TotalGenres = statisticsTotals.TotalGenres
            });
        }

        public IActionResult About()
            => View();

        public IActionResult Error()
            => View();
    }
}
