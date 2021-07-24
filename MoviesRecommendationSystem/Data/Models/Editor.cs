namespace MoviesRecommendationSystem.Data.Models
{
    using Microsoft.AspNetCore.Identity;
    using MoviesRecommendationSystem.Data.Models.ValidationAttributes;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using static DataConstants.Person;

    public class Editor
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(FirstNameMaxLength)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(LastNameMaxLength)]
        public string LastName { get; set; }

        [Required]
        public DateTime BirthDate { get; set; }

        [Required]
        public string UserId { get; set; }

        public IdentityUser User { get; set; }

        public IEnumerable<Movie> Movies { get; init; } = new List<Movie>();
    }
}
