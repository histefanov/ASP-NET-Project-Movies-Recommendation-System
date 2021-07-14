namespace MoviesRecommendationSystem.Infrastructure
{
    using System.Linq;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using MoviesRecommendationSystem.Data;
    using MoviesRecommendationSystem.Data.Models;

    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder PrepareDatabase(
            this IApplicationBuilder app)
        {
            using var scopedServices = app.ApplicationServices.CreateScope();

            var data = scopedServices.ServiceProvider.GetService<MoviesRecommendationDbContext>();

            data.Database.Migrate();

            SeedGenres(data);

            return app;
        }

        private static void SeedGenres(MoviesRecommendationDbContext data)
        {
            if (data.Genres.Any())
            {
                return;
            }

            data.Genres.AddRange(new[]
            {
                new Genre() { Name = "Action" },
                new Genre() { Name = "Horror" },
                new Genre() { Name = "Romance" },
                new Genre() { Name = "Drama" },
                new Genre() { Name = "Sci-Fi" },
                new Genre() { Name = "Comedy" },
                new Genre() { Name = "Thriller" },
                new Genre() { Name = "Western" },
                new Genre() { Name = "Documentary" },
                new Genre() { Name = "Animation" },
                new Genre() { Name = "War" },
                new Genre() { Name = "Fantasy" },
                new Genre() { Name = "Historical" },
            });

            data.SaveChanges();
        }
    }
}
