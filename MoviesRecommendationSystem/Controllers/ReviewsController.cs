namespace MoviesRecommendationSystem.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using MoviesRecommendationSystem.Infrastructure;
    using MoviesRecommendationSystem.Services.Reviews;

    public class ReviewsController : Controller
    {
        private readonly IReviewService reviewService;

        public ReviewsController(IReviewService reviewService) 
            => this.reviewService = reviewService;

        [Authorize]
        [HttpPost]
        public IActionResult Add(int movieId, int rating, string content)
        {
            var userId = this.User.GetId();

            this.reviewService.Create(
                userId, 
                movieId, 
                rating, 
                content);

            return RedirectToAction("Details", "Movies", new { id = movieId });
        }
    }
}
