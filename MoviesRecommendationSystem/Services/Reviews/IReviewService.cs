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

        IEnumerable<ReviewServiceModel> ReviewsForMovie(int id);
    }
}
