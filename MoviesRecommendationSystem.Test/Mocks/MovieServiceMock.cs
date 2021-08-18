namespace MoviesRecommendationSystem.Test.Mocks
{
    using Moq;

    using MoviesRecommendationSystem.Services.Movies;
    using MoviesRecommendationSystem.Services.Movies.Models;

    public class MovieServiceMock
    {
        public static IMovieService Instance
        {
            get
            {
                var serviceMock = new Mock<IMovieService>();

                serviceMock.Setup(s => s
                    .Random())
                    .Returns(new RandomMovieServiceModel
                    {
                        Title = "Title"
                    });

                return serviceMock.Object;
            }
        }
    }
}
