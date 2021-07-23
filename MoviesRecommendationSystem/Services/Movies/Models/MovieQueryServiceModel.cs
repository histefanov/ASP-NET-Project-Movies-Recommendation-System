namespace MoviesRecommendationSystem.Services.Movies.Models
{
    using System.Collections.Generic;

    public class MovieQueryServiceModel
    {
        public int CurrentPage { get; set; }

        public int MoviesPerPage { get; set; }

        public int TotalMovies { get; set; }

        public IEnumerable<MovieServiceModel> Movies { get; set; }
    }
}
