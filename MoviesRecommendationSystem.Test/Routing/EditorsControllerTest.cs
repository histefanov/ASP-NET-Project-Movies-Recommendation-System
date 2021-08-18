namespace MoviesRecommendationSystem.Test.Routing
{
    using Xunit;
    using MyTested.AspNetCore.Mvc;

    using MoviesRecommendationSystem.Controllers;
    using MoviesRecommendationSystem.Models.Editors;

    public class EditorsControllerTest
    {
        [Fact]
        public void GetBecomeShouldBeMappedAccurately()
            => MyRouting
                .Configuration()
                .ShouldMap("/Editors/Become")
                .To<EditorsController>(c => c.Become());

        [Fact]
        public void PostBecomeShouldBeMappedAccurately()
            => MyRouting
                .Configuration()
                .ShouldMap(request => request
                    .WithPath("/Editors/Become")
                    .WithMethod(HttpMethod.Post))
                .To<EditorsController>(c => c.Become(
                    With.Any<BecomeEditorFormModel>()));
    }
}
