namespace MoviesRecommendationSystem.Test.Controllers.Admin
{
    using System.Collections.Generic;
    using Xunit;
    using MyTested.AspNetCore.Mvc;

    using MoviesRecommendationSystem.Areas.Admin.Controllers;
    using MoviesRecommendationSystem.Services.Movies.Models;

    public class MovieControllerTests
    {
        [Fact]
        public void AllShouldRequireAdminAuthorizationAndReturnViewWithCorrectModel()
            => MyController<MoviesController>
                .Instance()
                .WithUser(user => user
                    .InRole("Admin"))
                .Calling(c => c.All())
                .ShouldReturn()
                .View(view => view
                    .WithModelOfType<List<MovieServiceModel>>());
    }
}
