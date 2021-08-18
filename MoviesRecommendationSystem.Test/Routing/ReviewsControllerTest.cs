namespace MoviesRecommendationSystem.Test.Routing
{
    using Xunit;
    using MyTested.AspNetCore.Mvc;
    
    using MoviesRecommendationSystem.Controllers;
    using MoviesRecommendationSystem.Models.Reviews;

    public class ReviewsControllerTest
    {
        [Fact]
        public void AddShouldBeMappedAccurately()
            => MyRouting
                .Configuration()
                .ShouldMap(request => request
                    .WithMethod(HttpMethod.Post)
                    .WithPath("/Reviews/Add"))
                .To<ReviewsController>(c => c.Add(
                    With.Any<AddReviewFormModel>()));

        [Fact]
        public void DeleteShouldBeMappedAccurately()
            => MyRouting
                .Configuration()
                .ShouldMap("/Reviews/Delete/1?movieId=1")
                .To<ReviewsController>(c => c.Delete(1, 1));
    }
}
