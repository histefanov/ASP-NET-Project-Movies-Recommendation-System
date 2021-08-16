namespace MoviesRecommendationSystem.Services.Movies.Models
{
    using System.Collections.Generic;
    using MoviesRecommendationSystem.Models.Reviews;
    using MoviesRecommendationSystem.Services.Reviews.Reviews;

    public class MovieDetailsServiceModel : MovieServiceModel
    {
        public int Runtime { get; set; }

        public string Language { get; set; }

        public string Studio { get; set; }

        public int DirectorId { get; set; }

        public string DirectorName { get; set; }

        public string StarringActors { get; set; }

        public int AverageRating { get; set; }

        public int EditorId { get; set; }

        public string EditorName { get; set; }

        public string YoutubeTrailerId { get; set; }

        public string PlaybackUrl { get; set; }

        public string ImdbId { get; set; }

        public string UserId { get; set; }

        public IEnumerable<ReviewServiceModel> Reviews { get; set; }

        public AddReviewFormModel ReviewFormModel { get; set; } = new AddReviewFormModel();
    }
}
