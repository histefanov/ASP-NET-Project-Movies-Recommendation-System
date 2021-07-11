namespace MoviesRecommendationSystem.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using static DataConstants;

    public class Movie
    {
        [Key]
        public int Id { get; init; }

        [Required]
        [MaxLength(MovieTitleMaxLength)]
        public string Title { get; set; }

        [Required]
        [Range(MovieReleaseYearMinValue, MovieReleaseYearMaxValue)]
        public int ReleaseYear { get; set; }

        [Required]
        [Range(MovieRuntimeMinValue, MovieRuntimeMaxValue)]
        public int Runtime { get; set; }

        [Required]
        [MaxLength(MoviePlotMaxLength)]
        public string Plot { get; set; }

        public string Language { get; set; }

        public int DirectorId { get; set; }

        public Director Director { get; set; }

        public int StudioId { get; set; }

        public Studio Studio { get; set; }
      
        public ICollection<MovieGenre> MovieGenres { get; init; } = new HashSet<MovieGenre>();

        public ICollection<MovieActor> MovieActors { get; init; } = new HashSet<MovieActor>();
    }
}
