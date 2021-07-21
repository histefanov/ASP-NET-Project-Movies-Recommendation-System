namespace MoviesRecommendationSystem.Models.Movies
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using static Data.DataConstants;

    public class AddMovieFormModel
    {       
        [Required]
        [StringLength(TitleMaxLength, MinimumLength = TitleMinLength)]
        public string Title { get; init; }

        [Display(Name = "Release Year")]
        [Required]
        [Range(
            YearMinValue, 
            YearMaxValue, 
            ErrorMessage = "The year of release must be between {1} and {2}.")]
        public int? ReleaseYear { get; init; }

        [Required]
        [Range(
            RuntimeMinValue, 
            RuntimeMaxValue,
            ErrorMessage = "The runtime must be between {1} and {2} minutes.")]
        public int? Runtime { get; init; }

        [Required]
        [StringLength(
            PlotMaxLength, 
            MinimumLength = PlotMinLength,
            ErrorMessage = "The plot description must be between {2} and {1} characters long.")]
        public string Plot { get; init; }

        [Required]
        [StringLength(
            LanguageMaxLength, 
            MinimumLength = LanguageMinLength,
            ErrorMessage = "The language must be between {2} and {1} characters long.")]
        public string Language { get; init; }

        [Display(Name = "Image URL")]
        [Required]
        [Url]
        public string ImageUrl { get; init; }

        [Required]
        [StringLength(FirstNameMaxLength + LastNameMaxLength, MinimumLength = FullNameMinLength)]
        public string Director { get; init; }

        [Display(Name = "Distribution Studio")]
        [Required]
        [StringLength(StudioNameMaxLength, MinimumLength = StudioNameMinLength)]
        public string Studio { get; init; }

        [Display(Name = "Starring Actors")]
        [Required]
        [RegularExpression(
            @"([A-Za-z .]+)(,\s*[A-Za-z .]+)*", 
            ErrorMessage = "Invalid format. Please type in the actors' names separated by commas.")]
        public string StarringActors { get; set; }

        [Required(ErrorMessage = "At least one genre selection is required.")]
        public List<string> Genres { get; set; }
    }
}
