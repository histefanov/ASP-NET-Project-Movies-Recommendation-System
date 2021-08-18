namespace MoviesRecommendationSystem.Test.Routing
{
    using Xunit;
    using MyTested.AspNetCore.Mvc;
    
    using MoviesRecommendationSystem.Controllers;
    using MoviesRecommendationSystem.Models.Movies;

    public class MoviesControllerTest
    {
        [Fact]
        public void GetAddShouldBeMappedAccurately()
            => MyRouting
                .Configuration()
                .ShouldMap("/Movies/Add")
                .To<MoviesController>(c => c.Add());

        [Fact]
        public void PostAddShouldBeMappedAccurately()
            => MyRouting
                .Configuration()
                .ShouldMap(request => request
                    .WithPath("/Movies/Add")
                    .WithMethod(HttpMethod.Post))
                .To<MoviesController>(c => c.Add(
                    With.Any<MovieFormModel>()));

        [Fact]

        public void GetEditShouldBeMappedAccurately()
            => MyRouting
                .Configuration()
                .ShouldMap("/Movies/Edit/1")
                .To<MoviesController>(c => c.Edit(1));

        [Fact]
        public void PostEditShouldBeMappedAccurately()
            => MyRouting
                .Configuration()
                .ShouldMap(request => request
                    .WithPath("/Movies/Edit/1")
                    .WithMethod(HttpMethod.Post))
                .To<MoviesController>(c => c.Edit(
                    1, With.Any<MovieFormModel>()));

        [Fact]
        public void DeleteShouldBeMappedAccurately()
            => MyRouting
                .Configuration()
                .ShouldMap("/Movies/Delete/1")
                .To<MoviesController>(c => c.Delete(1));

        [Fact]
        public void AllShouldBeMappedAccurately()
            => MyRouting
                .Configuration()
                .ShouldMap("/Movies/All")
                .To<MoviesController>(c => c.All(
                    With.Any<AllMoviesQueryModel>()));

        [Fact]
        public void EditorContributionsShouldBeMappedAccurately()
            => MyRouting
                .Configuration()
                .ShouldMap("/Movies/EditorContributions")
                .To<MoviesController>(c => c.EditorContributions());

        [Fact]
        public void DetailsShouldBeMappedAccurately()
            => MyRouting
                .Configuration()
                .ShouldMap("/Movies/Details/1/somemovie-2010")
                .To<MoviesController>(c => c.Details(1, "somemovie-2010"));
    }
}
