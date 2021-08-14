namespace MoviesRecommendationSystem.Test.Controllers
{
    using MoviesRecommendationSystem.Models.Editors;
    using MoviesRecommendationSystem.Controllers;
    using Xunit;
    using MyTested.AspNetCore.Mvc;
    using System;
    using MoviesRecommendationSystem.Data.Models;
    using System.Linq;

    public class EditorsControllerTest
    {
        [Fact]
        public void GetBecomeShouldRequireAuthorizationReturnCorrectView()
            => MyController<EditorsController>
                .Instance()
                .Calling(c => c.Become())
                .ShouldHave()
                .ActionAttributes(a => a
                    .RestrictingForAuthorizedRequests())
                .AndAlso()
                .ShouldReturn()
                .View();

        [Theory]
        [InlineData("TestFn", "TestLn", "01/04/1996")]
        public void PostBecomeShouldRequireAuthorizationAndReturnRedirectWithValidModel(
            string editorFirstName, string editorLastName, string editorBirthDateAsString)
        {
            var editorBirthDate = DateTime.Parse(editorBirthDateAsString);

            MyController<EditorsController>
                .Instance(i => i
                    .WithUser())
                .Calling(c => c.Become(new BecomeEditorFormModel
                {                    
                    FirstName = editorFirstName,
                    LastName = editorLastName,
                    BirthDate = editorBirthDate
                }))
                .ShouldHave()
                .ActionAttributes(a => a
                    .RestrictingForHttpMethod(HttpMethod.Post)
                    .RestrictingForAuthorizedRequests())
                .ValidModelState()
                .Data(d => d
                    .WithSet<Editor>(editors => editors
                        .Any(e =>
                            e.UserId == TestUser.Identifier &&
                            e.FirstName == editorFirstName &&
                            e.LastName == editorLastName &&
                            e.BirthDate == editorBirthDate)))
                .AndAlso()
                .ShouldReturn()
                .Redirect(r => r
                    .To<HomeController>(c => c.Index()));
        }
    }
}
