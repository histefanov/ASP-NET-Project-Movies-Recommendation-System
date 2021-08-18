namespace MoviesRecommendationSystem.Test.Services.Helpers
{
    using MoviesRecommendationSystem.Services.Movies;

    public static class DataSeeder
    {
        public static int SeedMovie(IMovieService movieService)
            => movieService.Create(
                "Title", 
                2010, 
                120, 
                "Some plot", 
                "English", 
                "someUrl", 
                "someUrl", 
                "someYoutubeId", 
                "someImdbId", 
                "Director", 
                "Studio", 
                "Some actors", 
                new[] { "1" }, 
                1, 
                true);
    }
}
