namespace MoviesRecommendationSystem.Infrastructure
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using MoviesRecommendationSystem.Data;
    using MoviesRecommendationSystem.Data.Models;

    using static Common.WebConstants;

    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder PrepareDatabase(
            this IApplicationBuilder app)
        {
            using var scopedServices = app.ApplicationServices.CreateScope();

            var serviceProvider = scopedServices.ServiceProvider;

            MigrateDatabase(serviceProvider);
            SeedGenres(serviceProvider);
            SeedAdmin(serviceProvider);

            return app;
        }

        private static void MigrateDatabase(IServiceProvider serviceProvider)
        {
            var data = serviceProvider.GetRequiredService<MoviesRecommendationDbContext>();

            data.Database.Migrate();
        }

        private static void SeedGenres(IServiceProvider serviceProvider)
        {
            var data = serviceProvider.GetRequiredService<MoviesRecommendationDbContext>();

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

        private static void SeedAdmin(IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<User>>();
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            Task
                .Run(async () =>
                {
                    if (await roleManager.RoleExistsAsync(AdminRoleName))
                    {
                        return;
                    }

                    await roleManager.CreateAsync(
                        new IdentityRole { Name = AdminRoleName });
                    
                    const string adminEmail = "admin@movies.com";
                    const string adminPassword = "adminkey";

                    var user = new User
                    {
                        Email = adminEmail,
                        UserName = adminEmail,
                        Name = "Admin"
                    };

                    await userManager.CreateAsync(user, adminPassword);

                    await userManager.AddToRoleAsync(user, AdminRoleName);
                })
                .GetAwaiter()
                .GetResult();
        }
    }
}
