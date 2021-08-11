namespace MoviesRecommendationSystem.Services.Reviews.Reviews
{
    using System;

    public class ReviewServiceModel
    {
        public string UserId { get; set; }

        public string Author { get; init; }

        public int Rating { get; init; }

        public string Content { get; init; }

        public DateTime DateCreated { get; init; }
    }
}
