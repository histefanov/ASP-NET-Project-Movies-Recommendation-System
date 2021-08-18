namespace MoviesRecommendationSystem.Test.Pipeline
{
    using Xunit;
    using MyTested.AspNetCore.Mvc;
    
    using MoviesRecommendationSystem.Controllers;

    public class EditorsControllerTest
    {
        [Fact]
        public void GetBecomeShouldRequireAuthorizationAndShouldReturnView()
            => MyMvc
                .Pipeline()
                .ShouldMap(request => request
                    .WithPath("/Editors/Become")
                    .WithUser())
                .To<EditorsController>(c => c.Become())
                .Which()
                .ShouldHave()
                .ActionAttributes(attributes => attributes
                    .RestrictingForAuthorizedRequests())
                .AndAlso()
                .ShouldReturn()
                .View();
    }
}
