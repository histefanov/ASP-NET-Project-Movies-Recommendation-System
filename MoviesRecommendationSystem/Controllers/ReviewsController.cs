namespace MoviesRecommendationSystem.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using MoviesRecommendationSystem.Infrastructure;
    using MoviesRecommendationSystem.Models.Reviews;
    using MoviesRecommendationSystem.Services.Movies;
    using MoviesRecommendationSystem.Services.Reviews;

    using static WebConstants;

    public class ReviewsController : Controller
    {
        private readonly IReviewService reviewService;
        private readonly IMovieService movieService;

        public ReviewsController(IReviewService reviewService, IMovieService movieService)
        {
            this.reviewService = reviewService;
            this.movieService = movieService;
        }

        [Authorize]
        [HttpPost]
        public IActionResult Add(AddReviewFormModel review)
        {
            if (!ModelState.IsValid)
            {
                TempData[GlobalMessageKey] = "Rating and content must be between 1 and 5 stars and between 5 and 200 characters respectively!";

                return RedirectToAction("Details", "Movies", new
                {
                    id = review.MovieId,
                    info = this.movieService.GetRouteInfo(review.MovieId)
                });
            }

            var userId = this.User.GetId();

            if (this.reviewService.UserHasReview(review.MovieId, userId))
            {
                TempData[GlobalMessageKey] = "You already have a review for this movie!";

                return RedirectToAction("Details", "Movies", new
                {
                    id = review.MovieId,
                    info = this.movieService.GetRouteInfo(review.MovieId)
                });
            }

            this.reviewService.Create(
                userId,
                review.MovieId,
                review.Rating,
                review.Content);

            return RedirectToAction("Details", "Movies", new
            {
                id = review.MovieId,
                info = this.movieService.GetRouteInfo(review.MovieId)
            });
        }       

        [Authorize]
        public IActionResult Delete(int id, int movieId)
        {
            var reviewIsByUser = this.reviewService.BelongsToUser(id, User.GetId());

            if (!User.IsAdmin() && !reviewIsByUser)
            {
                return BadRequest();
            }

            this.reviewService.Delete(id);

            var info = this.movieService.GetRouteInfo(movieId);

            return RedirectToAction(nameof(MoviesController.Details), "Movies", new 
            { 
                id = movieId,
                info
            });
        }
    }
}
