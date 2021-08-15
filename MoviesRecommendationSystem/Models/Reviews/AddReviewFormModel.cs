namespace MoviesRecommendationSystem.Models.Reviews
{
    using System.ComponentModel.DataAnnotations;

    using static Data.DataConstants.Review;

    public class AddReviewFormModel
    {
        public int MovieId { get; set; }

        [Required]
        [Range(MinRating, MaxRating)]
        public int Rating { get; init; }

        [Required]
        [StringLength(ContentMaxLength, MinimumLength = ContentMinLength,
            ErrorMessage = "Your review must be between 5 and 200 characters.")]
        public string Content { get; init; }
    }
}
