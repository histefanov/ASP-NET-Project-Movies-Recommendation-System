namespace MoviesRecommendationSystem.Services.Reviews
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using MoviesRecommendationSystem.Data;
    using MoviesRecommendationSystem.Data.Models;
    using MoviesRecommendationSystem.Services.Reviews.Reviews;

    public class ReviewService : IReviewService
    {
        private readonly MoviesRecommendationDbContext data;
        private readonly IMapper mapper;

        public ReviewService(MoviesRecommendationDbContext data, IMapper mapper)
        {
            this.data = data;
            this.mapper = mapper;
        }

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

        public IEnumerable<ReviewServiceModel> ReviewsForMovie(int id)
        {
            var hasReviews = this.data
                .Reviews
                .Any(r => r.MovieId == id);

            if (hasReviews)
            {
                return this.data
                .Reviews
                .Where(r => r.MovieId == id)
                .ProjectTo<ReviewServiceModel>(this.mapper.ConfigurationProvider)
                .OrderByDescending(r => r.DateCreated)
                .ToList();
            }
            else
            {
                return null;
            }
        }
    }
}
