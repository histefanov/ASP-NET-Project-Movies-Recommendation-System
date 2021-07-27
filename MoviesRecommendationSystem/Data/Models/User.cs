namespace MoviesRecommendationSystem.Data.Models
{
    using Microsoft.AspNetCore.Identity;
    using System.ComponentModel.DataAnnotations;

    using static DataConstants.Person;

    public class User : IdentityUser
    {
        [MaxLength(FullNameMaxLength)]
        public string Name { get; set; }
    }
}
