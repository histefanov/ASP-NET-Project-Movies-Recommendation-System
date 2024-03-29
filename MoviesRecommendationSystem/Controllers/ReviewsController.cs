﻿namespace MoviesRecommendationSystem.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using MoviesRecommendationSystem.Infrastructure;
    using MoviesRecommendationSystem.Models.Reviews;
    using MoviesRecommendationSystem.Services.Movies;
    using MoviesRecommendationSystem.Services.Reviews;

    using static WebConstants;
    using static Common.ControllerConstants.Reviews;

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
                TempData[GlobalMessageKey] = ReviewInvalidMessage;

                return RedirectToAction(nameof(MoviesController.Details), MoviesControllerName, new
                {
                    id = review.MovieId,
                    info = this.movieService.GetRouteInfo(review.MovieId)
                });
            }

            var userId = User.GetId();

            if (this.reviewService.UserHasReview(review.MovieId, userId))
            {
                TempData[GlobalMessageKey] = UserHasReviewMessage;

                return RedirectToAction(nameof(MoviesController.Details), MoviesControllerName, new
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

            TempData[GlobalMessageKey] = AddSuccessfulMessage;

            return RedirectToAction(nameof(MoviesController.Details), MoviesControllerName, new
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

            TempData[GlobalMessageKey] = DeleteSuccessfulMessage;

            return RedirectToAction(nameof(MoviesController.Details), MoviesControllerName, new 
            { 
                id = movieId,
                info
            });
        }
    }
}
