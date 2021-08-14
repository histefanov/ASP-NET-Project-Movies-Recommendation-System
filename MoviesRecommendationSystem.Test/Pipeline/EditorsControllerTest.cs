namespace MoviesRecommendationSystem.Test.Pipeline
{
    using System;
    using System.Linq;
    using MoviesRecommendationSystem.Controllers;
    using Xunit;
    using MyTested.AspNetCore.Mvc;
    using MoviesRecommendationSystem.Models.Editors;
    using MoviesRecommendationSystem.Data.Models;

    public class EditorsControllerTest
    {
        [Fact]
        public void GetBecomeShouldRequireAuthorizationAndShouldReturnView()
            => MyMvc
                .Pipeline()
                .ShouldMap(r => r
                    .WithPath("/Editors/Become")
                    .WithUser())
                .To<EditorsController>(c => c.Become())
                .Which()
                .ShouldHave()
                .ActionAttributes(a => a
                    .RestrictingForAuthorizedRequests())
                .AndAlso()
                .ShouldReturn()
                .View();
    }
}
