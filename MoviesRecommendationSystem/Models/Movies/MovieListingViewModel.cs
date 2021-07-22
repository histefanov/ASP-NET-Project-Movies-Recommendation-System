﻿namespace MoviesRecommendationSystem.Models.Movies
{
    using System.Collections.Generic;
    using MoviesRecommendationSystem.Data.Models;

    public class MovieListingViewModel
    {
        public int Id { get; init; }

        public string Title { get; init; }

        public int ReleaseYear { get; init; }

        public string Plot { get; init; }

        public string ImageUrl { get; init; }

        public IList<string> Genres { get; init; }
    }
}
