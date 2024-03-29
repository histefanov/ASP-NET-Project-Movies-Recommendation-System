﻿namespace MoviesRecommendationSystem.Models.Movies
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using MoviesRecommendationSystem.Data.Models.ValidationAttributes;
    using MoviesRecommendationSystem.Services.Movies.Models.Interfaces;

    using static Data.DataConstants.Movie;
    using static Data.DataConstants.Person;

    public class MovieFormModel : IMovieServiceModel
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

        [Display(Name = "Cover Image URL")]
        [Required]
        [Url(ErrorMessage = "Invalid Image URL.")]
        public string ImageUrl { get; init; }

        [Display(Name = "Playback URL")]
        [Required]
        [Url(ErrorMessage = "Invalid Playback URL.")]
        public string PlaybackUrl { get; init; }

        [Display(Name = "Youtube Trailer ID")]
        [Required]
        [RegularExpression(
            @"[a-zA-Z0-9_-]{11}", 
            ErrorMessage = "Please provide a valid video ID. It must be 11 characters long and may contain upper- and lowercase letters, digits, dashes and underscores.")]
        public string YoutubeTrailerId { get; init; }

        [Display(Name = "IMDb Identifier")]
        [Required]
        [RegularExpression(
            @"[a-zA-Z0-9]{9}",
            ErrorMessage = "Please provide a valid Imdb Identifier. It must be 9 characters long and may contain upper- and lowercase letters and digits.")]
        public string ImdbId { get; init; }

        [Display(Name = "Director")]
        [Required]
        [StringLength(FullNameMaxLength, MinimumLength = FullNameMinLength)]
        public string DirectorName { get; init; }

        [Display(Name = "Distribution Studio")]
        [Required]
        [StringLength(StudioNameMaxLength, MinimumLength = StudioNameMinLength)]
        public string Studio { get; init; }

        [Display(Name = "Starring Actors")]
        [Required]
        [StringLength((FullNameMaxLength * 3), MinimumLength = FullNameMinLength)]
        [RegularExpression(
            @"([A-Za-z .-]+)(,\s*[A-Za-z .-]+){0,2}", 
            ErrorMessage = "Invalid format. Please type in up to 3 actor names separated by commas.")]
        public string StarringActors { get; set; }

        [GenresCount(ErrorMessage = "You must select at least 1 and up to 3 genres.")]
        [Display(Name = "Genres")]
        public List<string> GenreIds { get; set; } = new List<string>();
    }
}
