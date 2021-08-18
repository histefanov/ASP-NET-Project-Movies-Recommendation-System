namespace MoviesRecommendationSystem.Test.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using Xunit;
    using MyTested.AspNetCore.Mvc;
    using Data;

    using MoviesRecommendationSystem.Controllers;
    using MoviesRecommendationSystem.Data.Models;
    using MoviesRecommendationSystem.Services.Movies.Models;

    public class WatchlistsControllerTest
    {
        [Theory]
        [InlineData(1, "TestId", "A Quiet Place", 2018)]
        public void AddShouldRequireAuthorizationAndReturnRedirectWithCorrectModel(
            int movieId, string userId, string title, int releaseYear)
            => MyController<WatchlistsController>
                .Instance()
                .WithUser()
                .WithData(Data.GetPublicMovie())
                .Calling(c => c.Add(movieId))
                .ShouldHave()
                    .ActionAttributes(attributes => attributes
                        .RestrictingForAuthorizedRequests())
                .AndAlso()
                .ShouldHave()
                .Data(data => data
                    .WithSet<UserWatchlistMovie>(watchlist => watchlist
                        .Any(w => 
                            w.MovieId == movieId &&
                            w.UserId == userId)))
                .AndAlso()
                .ShouldReturn()
                .Redirect(redirect => redirect
                    .To<MoviesController>(c => c.Details(1, $"{title}-{releaseYear}")));

        [Theory]
        [InlineData(1, "TestId", "A Quiet Place", 2018)]
        public void RemoveShouldRequireAuthorizationAndReturnRedirectWithCorrectModel(
            int movieId, string userId, string title, int releaseYear)
            => MyController<WatchlistsController>
                .Instance()
                .WithUser()
                .WithData(
                    Data.GetPublicMovie(),
                    Data.GetUserWatchlistMovie())
                .Calling(c => c.Remove(movieId))
                .ShouldHave()
                    .ActionAttributes(attributes => attributes
                        .RestrictingForAuthorizedRequests())
                .AndAlso()
                .ShouldHave()
                .Data(data => data
                    .WithSet<UserWatchlistMovie>(watchlist => !watchlist
                        .Any(w =>
                            w.MovieId == movieId &&
                            w.UserId == userId)))
                .AndAlso()
                .ShouldReturn()
                .Redirect(redirect => redirect
                    .To<MoviesController>(c => c.Details(1, $"{title}-{releaseYear}")));

        [Theory]
        [InlineData("TestId")]
        public void AllShouldRequireAuthorizationAndReturnViewWithCorrectModel(string userId)
            => MyController<WatchlistsController>
                .Instance()
                .WithUser()
                .WithData(
                    Data.GetPublicMovie(),
                    Data.GetUserWatchlistMovie())
                .Calling(c => c.All(userId))
                .ShouldHave()
                    .ActionAttributes(attributes => attributes
                        .RestrictingForAuthorizedRequests())
                .AndAlso()
                .ShouldReturn()
                .View(view => view
                    .WithModelOfType<IEnumerable<MovieServiceModel>>());
    }
}
