namespace MoviesRecommendationSystem.Data.Models.ValidationAttributes
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class GenresCountAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            var genres = (List<string>)value;

            return genres.Count > 0 && genres.Count < 4;
        }
    }
}
