namespace MoviesRecommendationSystem.Test.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using Xunit;

    using MoviesRecommendationSystem.Services.Reviews;
    using MoviesRecommendationSystem.Test.Mocks;
    using MoviesRecommendationSystem.Services.Reviews.Reviews;

    public class ReviewServiceTest
    {
        [Fact]
        public void CreateShouldAddReviewAndReturnReviewId()
        {
            // Arrange

            var data = DataMock.Instance;
            var mapper = MapperMock.Instance;
            var reviewService = new ReviewService(data, mapper);

            // Act

            var result = reviewService.Create(
                "userId",
                1,
                5,
                "awesome movie");

            // Assert

            Assert.IsType<int>(result);
            Assert.True(data.Reviews
                .Any(r => r.UserId == "userId"));
        }

        [Fact]
        public void DeleteShouldRemoveReview()
        {
            // Arrange

            var data = DataMock.Instance;
            var mapper = MapperMock.Instance;
            var reviewService = new ReviewService(data, mapper);

            var result = reviewService.Create(
                "userId",
                1,
                5,
                "awesome movie");

            // Act

            reviewService.Delete(1);

            // Assert

            Assert.IsType<int>(result);
            Assert.True(!data.Reviews
                .Any(r => r.UserId == "userId"));
        }

        [Fact]
        public void ReviewsForMovieShouldReturnCorrectModel()
        {
            // Arrange

            var data = DataMock.Instance;
            var mapper = MapperMock.Instance;
            var reviewService = new ReviewService(data, mapper);

            reviewService.Create(
                "userId",
                1,
                5,
                "awesome movie");

            // Act

            var result = reviewService.ReviewsForMovie(1);

            // Assert

            Assert.IsAssignableFrom<IEnumerable<ReviewServiceModel>>(result);
            Assert.NotNull(result);
        }

        [Fact]
        public void BelongsToUserShouldReturnBoolResult()
        {
            // Arrange

            var data = DataMock.Instance;
            var mapper = MapperMock.Instance;
            var reviewService = new ReviewService(data, mapper);

            // Act

            var result = reviewService.BelongsToUser(1, "someUser");

            // Assert

            Assert.IsType<bool>(result);
            Assert.False(result);
        }

        [Fact]
        public void UserHasReviewShouldReturnBoolResult()
        {
            // Arrange

            var data = DataMock.Instance;
            var mapper = MapperMock.Instance;
            var reviewService = new ReviewService(data, mapper);

            // Act

            var result = reviewService.UserHasReview(1, "someUser");

            // Assert

            Assert.IsType<bool>(result);
            Assert.False(result);
        }
    }
}
