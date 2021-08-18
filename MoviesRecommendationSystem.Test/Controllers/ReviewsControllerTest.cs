namespace MoviesRecommendationSystem.Test.Controllers
{
    using System.Linq;
    using Xunit;
    using MyTested.AspNetCore.Mvc;
    using Data;

    using MoviesRecommendationSystem.Controllers;
    using MoviesRecommendationSystem.Models.Reviews;
    using MoviesRecommendationSystem.Data.Models;

    public class ReviewsControllerTest
    {
        [Theory]
        [InlineData(1, 5, "Awesome movie", "A Quiet Place", 2018, "Your review was added successfully")]
        public void AddShouldRequireAuthorizationAndReturnRedirectWithValidModel(
            int movieId, int rating, string content, string title, int releaseYear, string tempDataMessage)
            => MyController<ReviewsController>
                .Instance()
                .WithUser()
                .WithData(Data.GetPublicMovie())
                .Calling(c => c.Add(new AddReviewFormModel
                {
                    MovieId = movieId,
                    Rating = rating,
                    Content = content
                }))
                .ShouldHave()
                    .ActionAttributes(attributes => attributes
                        .RestrictingForAuthorizedRequests())
                .AndAlso()
                .ShouldHave()
                .ValidModelState()
                .AndAlso()
                .ShouldHave()
                .TempData(tempData => tempData
                    .ContainingEntryWithValue(tempDataMessage))
                .AndAlso()
                .ShouldHave()
                .Data(data => data
                    .WithSet<Review>(reviews => reviews
                        .Any(r =>
                            r.MovieId == movieId &&
                            r.Rating == rating &&
                            r.Content == content)))
                .AndAlso()
                .ShouldReturn()
                .Redirect(redirect => redirect
                    .To<MoviesController>(c => c.Details(1, $"{title}-{releaseYear}")));

        [Theory]
        [InlineData(1, 5, "Awesome movie", "A Quiet Place", 2018, "You already have a review for this movie!")]
        public void AddShouldReturnRedirectWithValidModelWhenUserReviewAlreadyExists(
            int movieId, int rating, string content, string title, int releaseYear, string tempDataMessage)
            => MyController<ReviewsController>
                .Instance()
                .WithUser()
                .WithData(
                    Data.GetPublicMovie(),
                    Data.GetReview())
                .Calling(c => c.Add(new AddReviewFormModel
                {
                    MovieId = movieId,
                    Rating = rating,
                    Content = content
                }))
                .ShouldHave()
                    .ActionAttributes(attributes => attributes
                        .RestrictingForAuthorizedRequests())
                .AndAlso()
                .ShouldHave()
                .TempData(tempData => tempData
                    .ContainingEntryWithValue(tempDataMessage))
                .AndAlso()
                .ShouldReturn()
                .Redirect(redirect => redirect
                    .To<MoviesController>(c => c.Details(1, $"{title}-{releaseYear}")));

        [Theory]
        [InlineData(1, 1, "A Quiet Place", 2018, "Your review was removed successfully!")]
        public void DeleteShouldRequireAuthorizationAndReturnRedirectWithValidModel(
            int reviewId, int movieId, string title, int releaseYear, string tempDataMessage)
            => MyController<ReviewsController>
                .Instance()
                .WithUser()
                .WithData(
                    Data.GetPublicMovie(),
                    Data.GetReview())
                .Calling(c => c.Delete(reviewId, movieId))
                .ShouldHave()
                    .ActionAttributes(attributes => attributes
                        .RestrictingForAuthorizedRequests())
                .AndAlso()
                .ShouldHave()
                .Data(data => data
                    .WithSet<Review>(reviews => !reviews
                        .Any(r => r.Id == reviewId)))
                .AndAlso()
                .ShouldHave()
                .TempData(tempData => tempData
                    .ContainingEntryWithValue(tempDataMessage))
                .AndAlso()
                .ShouldReturn()
                .Redirect(redirect => redirect
                    .To<MoviesController>(c => c.Details(1, $"{title}-{releaseYear}")));
    }
}
