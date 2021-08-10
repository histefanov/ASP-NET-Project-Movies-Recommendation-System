namespace MoviesRecommendationSystem.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using MoviesRecommendationSystem.Data;

    public class ReviewsController : Controller
    {
        private readonly MoviesRecommendationDbContext data;

        public ReviewsController(MoviesRecommendationDbContext data) 
            => this.data = data;

        public IActionResult Add()
        {
            return View();
        }
    }
}
