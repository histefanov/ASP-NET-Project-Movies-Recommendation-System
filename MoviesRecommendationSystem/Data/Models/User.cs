namespace MoviesRecommendationSystem.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Microsoft.AspNetCore.Identity;

    using static DataConstants.Person;

    public class User : IdentityUser
    {
        [MaxLength(FullNameMaxLength)]
        public string Name { get; set; }

        public ICollection<UserWatchlistMovie> UserWatchlistMovies { get; init; }
            = new HashSet<UserWatchlistMovie>();
    }
}
