namespace MoviesRecommendationSystem.Services.Reviews
{
    public interface IReviewService
    {
        int Create(
            string userId,
            int movieId,
            int rating,
            string content);
    }
}
