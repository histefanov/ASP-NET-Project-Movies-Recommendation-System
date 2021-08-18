namespace MoviesRecommendationSystem.Test.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using Xunit;
    
    using MoviesRecommendationSystem.Test.Mocks;
    using MoviesRecommendationSystem.Services.Movies;
    using MoviesRecommendationSystem.Test.Services.Helpers;
    using MoviesRecommendationSystem.Services.Movies.Models;
    using MoviesRecommendationSystem.Data.Models;

    public class MovieServiceTest
    {
        [Fact]
        public void CreateShouldAddMovieAndReturnMovieId()
        {
            // Arrange
            var data = DataMock.Instance;
            var mapper = MapperMock.Instance;
            var movieService = new MovieService(data, mapper);

            // Act
            var result = DataSeeder.SeedMovie(movieService);

            // Assert
            Assert.IsType<int>(result);
            Assert.NotEmpty(data.Movies);
            Assert.Equal(1, data.Movies.FirstOrDefault().Id);
        }

        [Theory]
        [InlineData(1, "Title", 2010, 120, "Some plot", "English", "someUrl", "someUrl", "someYoutubeId", "someImdbId", "Director", "Studio", "Some actors", new[] { "1" }, false)]
        public void EditShouldReturnBoolResultWhenMovieIsEdited(
            int movieId,
            string title,
            int releaseYear,
            int runTime,
            string plot,
            string language,
            string imageUrl,
            string playbackUrl,
            string youtubeTrailerId,
            string imdbId,
            string director,
            string studio,
            string actors,
            IEnumerable<string> genres,
            bool IsPublic)
        {
            // Arrange
            var data = DataMock.Instance;
            var mapper = MapperMock.Instance;
            var movieService = new MovieService(data, mapper);

            DataSeeder.SeedMovie(movieService);

            // Act

            var result = movieService.Edit(
                movieId,
                title,
                releaseYear,
                runTime,
                plot,
                language,
                imageUrl,
                playbackUrl,
                youtubeTrailerId,
                imdbId,
                director,
                studio,
                actors,
                genres,
                IsPublic);

            // Assert

            Assert.IsType<bool>(result);
            Assert.True(result);
            Assert.NotEmpty(data.Movies);
        }

        [Fact]
        public void AllShouldReturnCorrectModel()
        {
            // Arrange

            var data = DataMock.Instance;
            var mapper = MapperMock.Instance;
            var movieService = new MovieService(data, mapper);

            var movieId = DataSeeder.SeedMovie(movieService);

            // Act

            var result = movieService.All();

            // Assert

            Assert.IsType<MovieQueryServiceModel>(result);
            Assert.NotEmpty(result.Movies);
        }

        [Fact]
        public void DeleteShouldReturnIntWhenMovieIsDeletedSuccessfully()
        {
            // Arrange

            var data = DataMock.Instance;
            var mapper = MapperMock.Instance;
            var movieService = new MovieService(data, mapper);

            var movieId = DataSeeder.SeedMovie(movieService);

            // Act

            var result = movieService.Delete(movieId);

            // Assert

            Assert.IsType<bool>(result);
            Assert.True(result);
            Assert.True(data
                .Movies
                .Find(movieId)
                .IsDeleted);
        }

        [Fact]
        public void LatestFourAddedMoviesShouldReturnCorrectModelAndNotBeEmpty()
        {
            // Arrange

            var data = DataMock.Instance;
            var mapper = MapperMock.Instance;
            var movieService = new MovieService(data, mapper);

            DataSeeder.SeedMovie(movieService);

            data.SaveChanges();

            // Act

            var result = movieService.LastFourAddedMovies();

            // Assert

            Assert.IsAssignableFrom<IEnumerable<MovieServiceModel>>(result);
            Assert.NotEmpty(result);
        }

        [Fact]
        public void RandomShouldReturnCorrectModelAndNotBeNull()
        {
            // Arrange

            var data = DataMock.Instance;
            var mapper = MapperMock.Instance;
            var movieService = new MovieService(data, mapper);

            var movieId = DataSeeder.SeedMovie(movieService);

            data.SaveChanges();

            // Act

            var result = movieService.Random();

            // Assert

            Assert.IsType<RandomMovieServiceModel>(result);
            Assert.NotNull(result);
            Assert.Equal(result.Id, movieId);
        }

        [Fact]
        public void DetailsShouldReturnCorrectModelAndNotBeNull()
        {
            // Arrange

            var data = DataMock.Instance;
            var mapper = MapperMock.Instance;
            var movieService = new MovieService(data, mapper);

            var movieId = DataSeeder.SeedMovie(movieService);

            data.SaveChanges();

            // Act

            var result = movieService.Details(movieId);

            // Assert

            Assert.IsType<MovieDetailsServiceModel>(result);
            Assert.NotNull(result);
            Assert.Equal(result.Id, movieId);
        }

        [Fact]
        public void FormDetailsShouldReturnCorrectModelAndNotBeNull()
        {
            // Arrange

            var data = DataMock.Instance;
            var mapper = MapperMock.Instance;
            var movieService = new MovieService(data, mapper);

            var movieId = DataSeeder.SeedMovie(movieService);

            data.SaveChanges();

            // Act

            var result = movieService.FormDetails(movieId);

            // Assert

            Assert.IsType<MovieFormDataServiceModel>(result);
            Assert.NotNull(result);
            Assert.Equal(result.Id, movieId);
            Assert.NotEqual(result.StarringActors, string.Empty);
        }

        [Fact]
        public void GetRouteInfoShouldReturnStringAndNotBeNull()
        {
            // Arrange

            var data = DataMock.Instance;
            var mapper = MapperMock.Instance;
            var movieService = new MovieService(data, mapper);

            var movieId = DataSeeder.SeedMovie(movieService);

            data.SaveChanges();

            // Act

            var result = movieService.GetRouteInfo(movieId);

            // Assert

            Assert.IsType<string>(result);
            Assert.NotNull(result);
            Assert.Equal("Title-2010", result);
        }

        [Fact]
        public void SwitchVisibilityShouldGiveOppositeIsPublicValue()
        {
            // Arrange

            var data = DataMock.Instance;
            var mapper = MapperMock.Instance;
            var movieService = new MovieService(data, mapper);

            var movieId = DataSeeder.SeedMovie(movieService);

            data.SaveChanges();

            // Act

            movieService.SwitchVisibility(movieId);

            var movie = data.Movies.Find(movieId);

            // Assert

            Assert.False(movie.IsPublic);
        }

        [Fact]
        public void AllGenresShouldReturnCorrectModelAndNotBeNull()
        {
            // Arrange

            var data = DataMock.Instance;
            var mapper = MapperMock.Instance;
            var movieService = new MovieService(data, mapper);

            data.Genres.Add(new Genre
            {
                Name = "Genre"
            });

            data.SaveChanges();

            // Act

            var result = movieService.AllGenres();

            // Assert

            Assert.IsAssignableFrom<IEnumerable<MovieGenreServiceModel>>(result);
            Assert.NotNull(result);
            Assert.NotEmpty(result);
            Assert.Single(result);
        }

        [Fact]
        public void SelectedGenreIdsShouldReturnCorrectModelAndNotBeNull()
        {
            // Arrange

            var data = DataMock.Instance;
            var mapper = MapperMock.Instance;
            var movieService = new MovieService(data, mapper);

            var movieId = DataSeeder.SeedMovie(movieService);

            data.Genres.Add(new Genre
            {
                Name = "Genre"
            });

            data.SaveChanges();

            // Act

            var result = movieService.SelectedGenreIds(movieId);

            // Assert

            Assert.IsAssignableFrom<IEnumerable<string>>(result);
            Assert.NotNull(result);
            Assert.NotEmpty(result);
            Assert.Single(result);
        }

        [Fact]
        public void ByUserShouldReturnCorrectModelAndNotBeNull()
        {
            // Arrange

            var data = DataMock.Instance;
            var mapper = MapperMock.Instance;
            var movieService = new MovieService(data, mapper);

            // Act

            var result = movieService.ByUser("user");

            // Assert

            Assert.IsAssignableFrom<IEnumerable<MovieServiceModel>>(result);
        }

        [Fact]
        public void IsByEditorShouldReturnBoolResult()
        {
            // Arrange

            var data = DataMock.Instance;
            var mapper = MapperMock.Instance;
            var movieService = new MovieService(data, mapper);

            // Act

            var result = movieService.IsByEditor(1, 1);

            // Assert

            Assert.IsType<bool>(result);
        }

        [Fact]
        public void GenreExistsShouldReturnBoolResultTrueIfExists()
        {
            // Arrange

            var data = DataMock.Instance;
            var mapper = MapperMock.Instance;
            var movieService = new MovieService(data, mapper);

            data.Add(new Genre
            {
                Name = "Genre"
            });

            data.SaveChanges();

            // Act

            var result = movieService.GenreExists(1);

            // Assert

            Assert.IsType<bool>(result);
            Assert.True(result);
        }
    }
}
