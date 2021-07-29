namespace MoviesRecommendationSystem.Areas.Admin.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public class MoviesController : AdminController
    {
        public IActionResult Index()
            => View();
    }
}
