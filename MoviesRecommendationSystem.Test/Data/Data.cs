namespace MoviesRecommendationSystem.Test.Data
{
    using System.Collections.Generic;
    using System.Linq;
    
    using MoviesRecommendationSystem.Data.Models;
    using MoviesRecommendationSystem.Models.Movies;

    public static class Data
    {
        public static IEnumerable<Movie> GetTenPublicMovies()
            => Enumerable.Range(0, 10).Select(x => new Movie
            {
                IsPublic = true
            });

        public static Movie GetPublicMovie()
            => new Movie
            {
                Title = "A Quiet Place",
                ReleaseYear = 2018,
                Runtime = 90,
                Language = "English",
                Studio = "Paramount Pictures",
                Plot = "An electrifying interpretation of the post-apocalyptic horror genre",
                DirectorId = 1,
                ImageUrl = "https://img.jakpost.net/c/2018/04/04/2018_04_04_43296_1522824740._large.jpg",
                PlaybackUrl = "https://www.amazon.com/Quiet-Place-Emily-Blunt/dp/B07BYJX9FZ",
                YoutubeTrailerId = "WR7cc5t7tv8",
                ImdbId = "tt6644200",
                IsDeleted = false,
                IsPublic = true,      
                EditorId = 1,             
            };

        public static Actor GetActor()
            => new Actor
            {
                Id = 1,
                Name = "Actor"
            };

        public static MovieActor GetMovieActor()
            => new MovieActor
            {
                MovieId = 1,
                ActorId = 1,
            };

        public static Genre GetGenre()
            => new Genre
            {
                Id = 1,
                Name = "Genre"
            };

        public static MovieGenre GetMovieGenre()
            => new MovieGenre
            {
                MovieId = 1,
                GenreId = 1
            };

        public static Director GetDirector()
            => new Director
            {
                Id = 1,
                Name = "Director",
            };

        public static Editor GetEditor()
            => new Editor
            {
                Id = 1,
                FirstName = "Editor",
                LastName = "Editor",
                UserId = "TestId"
            };

        public static MovieFormModel GetValidMovieFormModel()
            => new MovieFormModel
            {
                Title = "A Quiet Place",
                ReleaseYear = 2018,
                Runtime = 90,
                Plot = "An electrifying interpretation of the post-apocalyptic horror genre",
                Language = "English",
                ImageUrl = "https://img.jakpost.net/c/2018/04/04/2018_04_04_43296_1522824740._large.jpg",
                Studio = "Paramount Pictures",
                YoutubeTrailerId = "WR7cc5t7tv8",
                ImdbId = "tt6644200",
                PlaybackUrl = "https://www.amazon.com/Quiet-Place-Emily-Blunt/dp/B07BYJX9FZ",
                StarringActors = "ActorOne, ActorTwo",
                DirectorName = "Director",
                GenreIds = new List<string> { "1" }
            };
    }
}
