namespace MoviesRecommendationSystem.Services.Reviews
{
    using System.Collections.Generic;
    using MoviesRecommendationSystem.Services.Reviews.Reviews;

    public interface IReviewService
    {
        int Create(
            string userId,
            int movieId,
            int rating,
            string content);

        void Delete(int id);

        IEnumerable<ReviewServiceModel> ReviewsForMovie(int id);

        bool BelongsToUser(int reviewId, string userId);
    }
}
