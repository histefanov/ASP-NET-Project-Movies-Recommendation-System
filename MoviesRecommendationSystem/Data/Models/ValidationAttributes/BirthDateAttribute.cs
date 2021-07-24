namespace MoviesRecommendationSystem.Data.Models.ValidationAttributes
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using static DataConstants;

    public class BirthDateAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            DateTime date = Convert.ToDateTime(value);

            var dateTimeToday = DateTime.Today;

            var age = dateTimeToday.Year - date.Year;

            return age >= 16 && age <= 100;
        }
    }
}
