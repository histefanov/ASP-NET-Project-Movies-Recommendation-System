namespace MoviesRecommendationSystem.Test.Routing
{
    using Xunit;
    using MyTested.AspNetCore.Mvc;

    using MoviesRecommendationSystem.Controllers;

    public class WatchlistsControllerTest
    {
        [Fact]
        public void AddShouldBeMappedAccurately()
            => MyRouting
                .Configuration()
                .ShouldMap("Watchlist/Add/1")
                .To<WatchlistsController>(c => c.Add(1));

        [Fact]
        public void RemoveShouldBeMappedAccurately()
            => MyRouting
                .Configuration()
                .ShouldMap("Watchlist/Remove/1")
                .To<WatchlistsController>(c => c.Remove(1));

        [Fact]
        public void AllShouldBeMappedAccurately()
            => MyRouting
                .Configuration()
                .ShouldMap("Watchlists/All?userId=1")
                .To<WatchlistsController>(c => c.All("1"));
    }
}
