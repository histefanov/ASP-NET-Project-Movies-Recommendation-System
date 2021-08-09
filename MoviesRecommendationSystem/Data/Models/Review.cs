namespace MoviesRecommendationSystem.Data.Models
{
    using System;

    public class Review
    {
        public int Id { get; set; }

        public string UserId { get; set; }
        
        public User User { get; set; }

        public int MovieId { get; set; }

        public Movie Movie { get; set; }

        public string Content { get; set; }

        public double Rating { get; set; }

        public DateTime DateCreated { get; set; }
    }
}
