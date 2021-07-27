namespace MoviesRecommendationSystem.Controllers
{
    using System.Diagnostics;
    using Microsoft.AspNetCore.Mvc;
    using MoviesRecommendationSystem.Models;
    using MoviesRecommendationSystem.Models.Home;
    using MoviesRecommendationSystem.Services.Statistics;

    public class HomeController : Controller
    {
        private readonly IStatisticsService statistics;

        public HomeController(IStatisticsService statistics) 
            => this.statistics = statistics;

        public IActionResult Index()
        {
            var statisticsTotals = this.statistics.GetTotals();

            return View(new IndexViewModel
            {
                TotalMovies = statisticsTotals.TotalMovies,
                TotalDirectors = statisticsTotals.TotalDirectors,
                TotalActors = statisticsTotals.TotalActors,
                TotalGenres = statisticsTotals.TotalGenres
            });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() 
            => View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
