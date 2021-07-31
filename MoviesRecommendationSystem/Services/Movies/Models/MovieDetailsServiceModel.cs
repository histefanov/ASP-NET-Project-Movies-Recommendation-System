namespace MoviesRecommendationSystem.Services.Movies.Models
{
    public class MovieDetailsServiceModel : MovieServiceModel
    {
        public int Runtime { get; set; }

        public string Language { get; set; }

        public string Studio { get; set; }

        public int DirectorId { get; set; }

        public string DirectorName { get; set; }

        public string StarringActors { get; set; }

        public int EditorId { get; set; }

        public string EditorName { get; set; }

        public string YoutubeTrailerId { get; set; }

        public string UserId { get; set; }
    }
}
