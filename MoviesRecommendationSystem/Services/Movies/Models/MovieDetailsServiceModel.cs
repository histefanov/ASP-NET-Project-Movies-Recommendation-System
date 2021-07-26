namespace MoviesRecommendationSystem.Services.Movies.Models
{
    public class MovieDetailsServiceModel : MovieServiceModel
    {
        public int EditorId { get; set; }

        public string EditorName { get; set; }

        public string UserId { get; set; }
    }
}
