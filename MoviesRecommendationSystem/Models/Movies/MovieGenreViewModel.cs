namespace MoviesRecommendationSystem.Models.Movies
{
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Mvc.Rendering;

    public class MovieGenreViewModel
    {
        public int Id { get; init; }

        public string Name { get; init; }

        public bool IsSelected { get; set; }
    }
}
