namespace MoviesRecommendationSystem.Test.Routing.Admin
{
    using MyTested.AspNetCore.Mvc;
    using Xunit;

    using MoviesRecommendationSystem.Areas.Admin.Controllers;

    public class MoviesControllerTest
    {
        [Fact]
        public void AllShouldBeMappedAccurately()
            => MyRouting
                .Configuration()
                .ShouldMap("/Admin/Movies/All")
                .To<MoviesController>(c => c.All());

        [Fact]
        public void SwitchVisibilityShouldBeMappedAccurately()
            => MyRouting
                .Configuration()
                .ShouldMap("/Admin/Movies/SwitchVisibility/1")
                .To<MoviesController>(c => c.SwitchVisibility(1));
    }
}
