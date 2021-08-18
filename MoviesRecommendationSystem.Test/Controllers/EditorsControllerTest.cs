namespace MoviesRecommendationSystem.Test.Controllers
{
    using System;
    using System.Linq;
    using Xunit;
    using MyTested.AspNetCore.Mvc;

    using MoviesRecommendationSystem.Models.Editors;
    using MoviesRecommendationSystem.Controllers;
    using MoviesRecommendationSystem.Data.Models;

    public class EditorsControllerTest
    {
        [Fact]
        public void GetBecomeShouldRequireAuthorizationAndReturnCorrectView()
            => MyController<EditorsController>
                .Instance()
                .Calling(c => c.Become())
                .ShouldHave()
                .ActionAttributes(attributes => attributes
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
                .Instance(instance => instance
                    .WithUser())
                .Calling(c => c.Become(new BecomeEditorFormModel
                {                    
                    FirstName = editorFirstName,
                    LastName = editorLastName,
                    BirthDate = editorBirthDate
                }))
                .ShouldHave()
                .ActionAttributes(attributes => attributes
                    .RestrictingForHttpMethod(HttpMethod.Post)
                    .RestrictingForAuthorizedRequests())
                .AndAlso()
                .ShouldHave()
                .ValidModelState()
                .AndAlso()
                .ShouldHave()
                .Data(data => data
                    .WithSet<Editor>(editors => editors
                        .Any(e =>
                            e.UserId == TestUser.Identifier &&
                            e.FirstName == editorFirstName &&
                            e.LastName == editorLastName &&
                            e.BirthDate == editorBirthDate)))
                .AndAlso()
                .ShouldReturn()
                .Redirect(redirect => redirect
                    .To<HomeController>(c => c.Index()));
        }
    }
}
