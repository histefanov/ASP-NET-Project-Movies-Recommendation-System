namespace MoviesRecommendationSystem.Models.Editors
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using MoviesRecommendationSystem.Data.Models.ValidationAttributes;

    using static Data.DataConstants.Person;

    public class BecomeEditorFormModel
    {
        [Required]
        [StringLength(FirstNameMaxLength, MinimumLength = FirstNameMinLength)]
        [Display(Name = "First name")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(LastNameMaxLength, MinimumLength = LastNameMinLength)]
        [Display(Name = "Last name")]
        public string LastName { get; set; }

        [Required]
        [BirthDate(ErrorMessage = "Your age must be between 16 and 100 years old.")]
        [Display(Name = "Date of birth")]
        public DateTime BirthDate { get; set; }
    }
}
