namespace MoviesRecommendationSystem.Services.Reviews.Reviews
{
    using System;

    public class ReviewServiceModel
    {
        public int Id { get; init; }

        public string UserId { get; init; }

        public string Author { get; init; }

        public int Rating { get; init; }

        public string Content { get; init; }

        public DateTime DateCreated { get; init; }
    }
}
