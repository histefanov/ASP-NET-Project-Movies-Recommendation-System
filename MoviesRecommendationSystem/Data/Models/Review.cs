﻿namespace MoviesRecommendationSystem.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using static DataConstants.Review;

    public class Review
    {
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; }
        
        public User User { get; set; }

        [Required]
        public int MovieId { get; set; }

        public Movie Movie { get; set; }

        [Required]
        [MaxLength(ContentMaxLength)]
        public string Content { get; set; }

        [Range(MinRating, MaxRating)]
        public int Rating { get; set; }

        public DateTime DateCreated { get; set; }
    }
}
