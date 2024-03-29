﻿namespace MoviesRecommendationSystem.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using static DataConstants.Movie;

    public class Movie
    {
        [Key]
        public int Id { get; init; }

        [Required]
        [MaxLength(TitleMaxLength)]
        public string Title { get; set; }

        [Required]
        [Range(YearMinValue, YearMaxValue)]
        public int ReleaseYear { get; set; }

        [Required]
        [Range(RuntimeMinValue, RuntimeMaxValue)]
        public int Runtime { get; set; }

        [Required]
        [MaxLength(PlotMaxLength)]
        public string Plot { get; set; }

        [Required]
        [MaxLength(LanguageMaxLength)]
        public string Language { get; set; }

        [Required]
        [Url]
        public string ImageUrl { get; set; }

        public int DirectorId { get; set; }

        public Director Director { get; set; }

        [Required]
        [MaxLength(StudioNameMaxLength)]
        public string Studio { get; set; }

        public int? EditorId { get; set; }

        public Editor Editor { get; init; }

        public string YoutubeTrailerId { get; set; }

        public string ImdbId { get; set; }

        [Url]
        public string PlaybackUrl { get; set; }

        public bool IsPublic { get; set; }

        public bool IsDeleted { get; set; }
      
        public ICollection<MovieGenre> MovieGenres { get; init; } = new HashSet<MovieGenre>();

        public ICollection<MovieActor> MovieActors { get; init; } = new HashSet<MovieActor>();

        public ICollection<UserWatchlistMovie> UserWatchlistMovies { get; init; }
            = new HashSet<UserWatchlistMovie>();

        public ICollection<Review> Reviews { get; init; } = new HashSet<Review>();
    }
}
