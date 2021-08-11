namespace MoviesRecommendationSystem.Services.Reviews
{
    using System;
    using MoviesRecommendationSystem.Data;
    using MoviesRecommendationSystem.Data.Models;

    public class ReviewService : IReviewService
    {
        private readonly MoviesRecommendationDbContext data;

        public ReviewService(MoviesRecommendationDbContext data) 
            => this.data = data;

        public int Create(
            string userId,
            int movieId,
            int rating,
            string content)
        {
            var reviewData = new Review
            {
                UserId = userId,
                MovieId = movieId,
                Content = content,
                Rating = rating,
                DateCreated = DateTime.UtcNow
            };

            this.data.Reviews.Add(reviewData);
            this.data.SaveChanges();

            return reviewData.Id;
        }
    }
}
