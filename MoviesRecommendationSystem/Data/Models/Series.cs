namespace MoviesRecommendationSystem.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using static DataConstants;

    public class Series
    {
        [Key]
        public int Id { get; init; }

        [Required]
        [MaxLength(TitleMaxLength)]
        public string Title { get; set; }

        [Required]
        [Range(YearMinValue, YearMaxValue)]
        public int ReleaseYear { get; set; }

        [Range(YearMinValue, YearMaxValue)]
        public int EndYear { get; set; }

        [Required]
        [Range(SeasonCountMinValue, SeasonCountMaxValue)]
        public int SeasonCount { get; set; }

        [Required]
        [Range(RuntimeMinValue, RuntimeMaxValue)]
        public int AverageRuntime { get; set; }

        [Required]
        [MaxLength(PlotMaxLength)]
        public string Plot { get; set; }

        [Required]
        [MaxLength(LanguageMaxLength)]
        public string Language { get; set; }

        [Required]
        [Url]
        public string ImageUrl { get; set; }

        public int StudioId { get; set; }

        public string Studio { get; set; }

        public ICollection<SeriesGenre> SeriesGenres { get; init; } = new HashSet<SeriesGenre>();

        public ICollection<SeriesActor> SeriesActors { get; init; } = new HashSet<SeriesActor>();
    }
}
