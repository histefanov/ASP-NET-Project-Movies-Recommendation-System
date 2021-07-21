namespace MoviesRecommendationSystem.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using static DataConstants;

    public class Actor
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(FirstNameMaxLength)]
        public string FirstName { get; set; }

        [MaxLength(MiddleNameMaxLength)]
        public string MiddleName { get; set; }

        [Required]
        [MaxLength(LastNameMaxLength)]
        public string LastName { get; set; }

        public ICollection<MovieActor> MovieActors { get; init; } = new HashSet<MovieActor>();
    }
}
