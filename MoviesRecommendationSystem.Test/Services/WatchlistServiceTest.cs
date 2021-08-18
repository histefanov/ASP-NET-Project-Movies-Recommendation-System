namespace MoviesRecommendationSystem.Test.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using Xunit;

    using MoviesRecommendationSystem.Test.Mocks;
    using MoviesRecommendationSystem.Services.Watchlists;
    using MoviesRecommendationSystem.Services.Movies.Models;
    using MoviesRecommendationSystem.Services.Watchlists.Models;

    public class WatchlistServiceTest
    {
        [Fact]
        public void AddShouldCreateUserWatchlistMovieAndReturnBoolResult()
        {

            // Arrange

            var data = DataMock.Instance;
            var mapper = MapperMock.Instance;
            var watchlistService = new WatchlistService(data, mapper);

            // Act

            var result = watchlistService.Add("someId", 1);

            // Assert

            Assert.IsType<bool>(result);
            Assert.True(result);
            Assert.True(data.UserWatchlistMovies.Any(w => w.UserId == "someId"));
        }

        [Fact]
        public void RemoveShouldRemoveUserWatchlistMovieAndReturnBoolResult()
        {

            // Arrange

            var data = DataMock.Instance;
            var mapper = MapperMock.Instance;
            var watchlistService = new WatchlistService(data, mapper);

            watchlistService.Add("someId", 1);

            // Act

            var result = watchlistService.Remove("someId", 1);

            // Assert

            Assert.IsType<bool>(result);
            Assert.True(result);
            Assert.False(data.UserWatchlistMovies.Any(w => w.UserId == "someId"));
        }

        [Fact]
        public void RemoveForAllUsersShouldRemoveAllUserWatchlistMoviesWithGivenUserId()
        {

            // Arrange

            var data = DataMock.Instance;
            var mapper = MapperMock.Instance;
            var watchlistService = new WatchlistService(data, mapper);

            watchlistService.Add("someId", 1);
            watchlistService.Add("someId2", 1);

            // Act

            watchlistService.RemoveForAllUsers(1);

            // Assert
           
            Assert.False(data.UserWatchlistMovies.Any(w => w.UserId == "someId" || w.UserId == "someId2"));
        }

        [Fact]
        public void CountShouldReturnWatchlistCountForGivenUserId()
        {

            // Arrange

            var data = DataMock.Instance;
            var mapper = MapperMock.Instance;
            var watchlistService = new WatchlistService(data, mapper);

            watchlistService.Add("someId", 1);
            watchlistService.Add("someId", 2);

            // Act

            var result = watchlistService.Count("someId");

            // Assert

            Assert.IsType<int>(result);
            Assert.Equal(2, result);
        }

        [Fact]
        public void ExistsShouldReturnBoolResult()
        {

            // Arrange

            var data = DataMock.Instance;
            var mapper = MapperMock.Instance;
            var watchlistService = new WatchlistService(data, mapper);

            watchlistService.Add("someId", 1);

            // Act

            var result = watchlistService.Exists("someId", 1);

            // Assert

            Assert.IsType<bool>(result);
            Assert.True(result);
        }

        [Fact]
        public void GetMoviesByUserShouldReturnCorrectModel()
        {

            // Arrange

            var data = DataMock.Instance;
            var mapper = MapperMock.Instance;
            var watchlistService = new WatchlistService(data, mapper);

            watchlistService.Add("someId", 1);

            // Act

            var result = watchlistService.GetMoviesByUser("someId");

            // Assert

            Assert.IsAssignableFrom<IEnumerable<WatchlistMovieServiceModel>>(result);
        }

        [Fact]
        public void GetMoviesByUserDetailedShouldReturnCorrectModel()
        {

            // Arrange

            var data = DataMock.Instance;
            var mapper = MapperMock.Instance;
            var watchlistService = new WatchlistService(data, mapper);

            watchlistService.Add("someId", 1);

            // Act

            var result = watchlistService.GetMoviesDetailedByUser("someId");

            // Assert

            Assert.IsAssignableFrom<IEnumerable<MovieServiceModel>>(result);
        }
    }
}
