namespace MoviesRecommendationSystem.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using static DataConstants.Person;

    public class Director
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(FullNameMaxLength)]
        public string Name { get; set; }

        public ICollection<Movie> Movies { get; init; } = new HashSet<Movie>();
    }
}
