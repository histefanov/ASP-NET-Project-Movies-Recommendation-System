namespace MoviesRecommendationSystem.Services.Movies.Models
{
    using System.Collections.Generic;

    public class RandomMovieServiceModel : MovieServiceModel
    {
        public int Runtime { get; set; }

        public string Language { get; set; }

        public string Studio { get; set; }

        public string DirectorName { get; set; }

        public IEnumerable<string> StarringActors { get; set; }

        public string YoutubeTrailerId { get; set; }

        public string ImdbId { get; set; }
    }
}
