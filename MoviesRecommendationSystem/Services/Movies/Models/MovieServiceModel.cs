namespace MoviesRecommendationSystem.Services.Movies.Models
{
    using MoviesRecommendationSystem.Services.Movies.Models.Interfaces;
    using System.Collections.Generic;
    
    public class MovieServiceModel : IMovieServiceModel
    {
        public int Id { get; init; }

        public string Title { get; init; }

        public int? ReleaseYear { get; init; }

        public string Plot { get; init; }

        public string ImageUrl { get; init; }

        public bool IsPublic { get; init; }

        public bool IsDeleted { get; init; }

        public IList<string> Genres { get; init; }
    }
}
