namespace MoviesRecommendationSystem.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Review
    {
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; }
        
        public User User { get; set; }

        [Required]
        public int MovieId { get; set; }

        public Movie Movie { get; set; }

        public string Content { get; set; }

        public int Rating { get; set; }

        public DateTime DateCreated { get; set; }
    }
}
