namespace MoviesRecommendationSystem.Test.Controllers
{
    using System.Linq;
    using Xunit;
    using MyTested.AspNetCore.Mvc;
    using Data;
    
    using MoviesRecommendationSystem.Controllers;
    using MoviesRecommendationSystem.Data.Models;

    public class MoviesControllerTest
    {
        [Fact]
        public void GetAddShouldRequireAuthorizationAndReturnCorrectView()
            => MyController<MoviesController>
                .Instance()
                .WithUser()
                .WithData(Data.GetEditor())
                .Calling(c => c.Add())
                .ShouldHave()
                    .ActionAttributes(attributes => attributes
                        .RestrictingForAuthorizedRequests())
                .AndAlso()
                .ShouldReturn()
                .View();

        [Theory]
        [InlineData("A Quiet Place", 2018)]
        public void PostAddShouldRequireAuthorizionAndRedirectWithValidModel(string title, int releaseYear)
            => MyController<MoviesController>
                .Instance(instance => instance
                    .WithUser()
                    .WithData(
                        Data.GetEditor(),
                        Data.GetGenre()))
                .Calling(c => c.Add(
                    Data.GetValidMovieFormModel()))
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
                    .WithSet<Movie>(movies => movies
                        .Any(m =>
                            m.Title == title &&
                            m.ReleaseYear == releaseYear)))
                .AndAlso()
                .ShouldReturn()
                .Redirect(redirect => redirect
                    .To<MoviesController>(c => c.Details(1, $"{title}-{releaseYear}")));

        [Theory]
        [InlineData(1)]
        public void GetEditShouldRequireAuthorizationAndReturnCorrectView(int movieId)
            => MyController<MoviesController>
                .Instance()
                    .WithUser()
                    .WithData(
                        Data.GetEditor(),
                        Data.GetGenre(),
                        Data.GetDirector(),
                        Data.GetActor(),
                        Data.GetPublicMovie(),
                        Data.GetMovieActor(),
                        Data.GetMovieGenre())
                .Calling(c => c.Edit(movieId))
                .ShouldHave()
                .ActionAttributes(attributes => attributes
                    .RestrictingForAuthorizedRequests())
                .AndAlso()
                .ShouldReturn()
                .View();

        [Theory]
        [InlineData("A Quiet Place", 2018)]
        public void PostEditShouldRequireAuthorizionAndRedirectWithValidModel(string title, int releaseYear)
            => MyController<MoviesController>
                .Instance(instance => instance
                    .WithUser()
                    .WithData(
                        Data.GetPublicMovie(),
                        Data.GetEditor(),
                        Data.GetGenre()))
                .Calling(c => c.Edit(
                    1,
                    Data.GetValidMovieFormModel()))
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
                    .WithSet<Movie>(movies => movies
                        .Any(m =>
                            m.Title == title &&
                            m.ReleaseYear == releaseYear)))
                .AndAlso()
                .ShouldReturn()
                .Redirect(redirect => redirect
                    .To<MoviesController>(c => c.Details(1, $"{title}-{releaseYear}")));

        [Theory]
        [InlineData(1)]
        public void DeleteShouldRequireAuthorizationAndReturnCorrectView(int movieId)
            => MyController<MoviesController>
                .Instance()
                .WithUser()
                .WithData(
                    Data.GetEditor(),
                    Data.GetPublicMovie())
                .Calling(c => c.Delete(movieId))
                .ShouldHave()
                    .ActionAttributes(attributes => attributes
                        .RestrictingForAuthorizedRequests())
                .AndAlso()
                .ShouldHave()
                .Data(data => data
                    .WithSet<Movie>(movies => movies
                        .Any(m =>
                            m.Id == 1 &&
                            m.IsDeleted)))
                .AndAlso()
                .ShouldReturn()
                .Redirect(redirect => redirect
                    .To<MoviesController>(c => c.EditorContributions()));
    }
}
