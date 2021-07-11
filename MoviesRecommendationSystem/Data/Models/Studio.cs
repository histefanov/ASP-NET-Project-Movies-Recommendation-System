namespace MoviesRecommendationSystem.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using static DataConstants;

    public class Studio
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(StudioNameMaxLength)]
        public string Name { get; set; }

        public ICollection<Movie> Movies { get; init; } = new HashSet<Movie>();
    }
}
