namespace MoviesRecommendationSystem.Services.Movies.Models
{
    public class MovieFormDataServiceModel : MovieServiceModel
    {
        public int Runtime { get; set; }

        public string Language { get; set; }

        public string Studio { get; set; }

        public string DirectorName { get; set; }

        public string StarringActors { get; set; }

        public string YoutubeTrailerId { get; set; }

        public string PlaybackUrl { get; set; }

        public string ImdbId { get; set; }

        public string UserId { get; set; }
    }
}
