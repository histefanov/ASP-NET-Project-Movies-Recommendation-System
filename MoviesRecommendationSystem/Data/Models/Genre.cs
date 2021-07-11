namespace MoviesRecommendationSystem.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using static DataConstants;

    public class Genre
    {
        [Key]
        public int Id { get; init; }

        [Required]
        [MaxLength(GenreNameMaxLength)]
        public string Name { get; set; }

        public ICollection<MovieGenre> MovieGenres { get; init; } = new HashSet<MovieGenre>();
    }
}
