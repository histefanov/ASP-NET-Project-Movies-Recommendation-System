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

    using static Areas.Admin.AdminConstants;

    public static class ApplicationBuilderExtensions
    {
        private const string AdminEmail = "admin@movies.com";

        public static IApplicationBuilder PrepareDatabase(
            this IApplicationBuilder app)
        {
            using var scopedServices = app.ApplicationServices.CreateScope();

            var serviceProvider = scopedServices.ServiceProvider;
            
            MigrateDatabase(serviceProvider);

            SeedGenres(serviceProvider);
            SeedDirectors(serviceProvider);
            SeedActors(serviceProvider);
            SeedMovies(serviceProvider);
            SeedMovieActors(serviceProvider);
            SeedMovieGenres(serviceProvider);

            SeedAdmin(serviceProvider);
            SeedAdminEditor(serviceProvider);

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

        private static void SeedDirectors(IServiceProvider serviceProvider)
        {
            var data = serviceProvider.GetRequiredService<MoviesRecommendationDbContext>();

            if (data.Directors.Any())
            {
                return;
            }

            data.Directors.AddRange(new[]
            {
                new Director { Name = "Steven Spielberg" },
                new Director { Name = "Christopher Nolan" },
                new Director { Name = "Alfred Hitchcock" },
            });

            data.SaveChanges();
        }

        private static void SeedActors(IServiceProvider serviceProvider)
        {
            var data = serviceProvider.GetRequiredService<MoviesRecommendationDbContext>();

            if (data.Actors.Any())
            {
                return;
            }

            data.Actors.AddRange(new[]
            {
                new Actor { Name = "John Krasinski" },
                new Actor { Name = "Penelope Cruz" },
                new Actor { Name = "Andy Garcia" },
            });

            data.SaveChanges();
        }

        private static void SeedMovies(IServiceProvider serviceProvider)
        {
            var data = serviceProvider.GetRequiredService<MoviesRecommendationDbContext>();

            if (data.Movies.Any())
            {
                return;
            }

            data.Movies.AddRange(new[]
            {
                new Movie() { 
                    Title = "A Quiet Place", 
                    ReleaseYear = 2018, 
                    Runtime = 90, 
                    Plot = "An electrifying interpretation of the post-apocalyptic horror genre", 
                    Language = "English", 
                    ImageUrl = "https://img.jakpost.net/c/2018/04/04/2018_04_04_43296_1522824740._large.jpg", 
                    DirectorId = 1, 
                    Studio = "Paramount Pictures", 
                    EditorId = null, 
                    YoutubeTrailerId = "WR7cc5t7tv8", 
                    ImdbId = "tt6644200", 
                    IsDeleted = false, 
                    IsPublic = true, 
                    PlaybackUrl = "https://www.amazon.com/Quiet-Place-Emily-Blunt/dp/B07BYJX9FZ" },

                new Movie() { 
                    Title = "In Bruges", 
                    ReleaseYear = 2008, 
                    Runtime = 107, 
                    Plot = "You don't want to be in Bruges if you're a hitman", 
                    Language = "English", 
                    ImageUrl = "https://miro.medium.com/max/1200/1*_ycbwmbbdcjMoc67Q7qSvA.jpeg", 
                    DirectorId = 2, 
                    Studio = "Universal Pictures", 
                    EditorId = null, 
                    YoutubeTrailerId = "p-gG2qo_l_A", 
                    ImdbId = "tt0780536", 
                    IsDeleted = false, 
                    IsPublic = true, 
                    PlaybackUrl = "https://www.netflix.com/title/70083111" },

                new Movie() {
                    Title = "The Nice Guys",
                    ReleaseYear = 2016,
                    Runtime = 116,
                    Plot = "A perfectly mismatched pair of private detectives working on a bizarre case",
                    Language = "English",
                    ImageUrl = "https://fightinginthewarroom.com/wp-content/uploads/2016/05/maxresdefault-1.jpg",
                    DirectorId = 3,
                    Studio = "Warner Bros. Pictures",
                    EditorId = null,
                    YoutubeTrailerId = "GQR5zsLHbYw",
                    ImdbId = "tt3799694",
                    IsDeleted = false,
                    IsPublic = true,
                    PlaybackUrl = "https://www.netflix.com/title/80049284" },

                new Movie() {
                    Title = "The Pianist",
                    ReleaseYear = 2002,
                    Runtime = 150,
                    Plot = "A Polish Jewish musician struggles to survive the destruction of the Warsaw ghetto of World War II",
                    Language = "English, German",
                    ImageUrl = "https://war-documentary.info/wp-content/uploads/2020/05/the-pianist-main.jpg",
                    DirectorId = 1,
                    Studio = "BAC Films",
                    EditorId = null,
                    YoutubeTrailerId = "u_jE7-6Uv7E",
                    ImdbId = "tt0253474",
                    IsDeleted = false,
                    IsPublic = true,
                    PlaybackUrl = "https://www.netflix.com/title/60025061" },

                new Movie() {
                    Title = "Zombieland",
                    ReleaseYear = 2009,
                    Runtime = 88,
                    Plot = "A shy student, a gun-toting bruiser and a pair of sisters join forces in a zombie-filled world",
                    Language = "English",
                    ImageUrl = "https://images-na.ssl-images-amazon.com/images/S/pv-target-images/73dc0509627616962b0cba3382ebb287cb756d1b901f16cf03947f06c7ba709b._V_SX1080_.jpg",
                    DirectorId = 2,
                    Studio = "Sony Pictures",
                    EditorId = null,
                    YoutubeTrailerId = "8m9EVP8X7N8",
                    ImdbId = "tt1156398",
                    IsDeleted = false,
                    IsPublic = true,
                    PlaybackUrl = "https://www.netflix.com/ca/title/70123542" },

                new Movie() {
                    Title = "Avengers: Infinity War",
                    ReleaseYear = 2018,
                    Runtime = 159,
                    Plot = "Emotional rollercoaster of a superhero movie packed with everything",
                    Language = "English",
                    ImageUrl = "https://pbs.twimg.com/media/ESrHsjLUYAA1LM6.jpg:large",
                    DirectorId = 3,
                    Studio = "Walt Disney Studios",
                    EditorId = null,
                    YoutubeTrailerId = "6ZfuNTqbHE8",
                    ImdbId = "tt4154756",
                    IsDeleted = false,
                    IsPublic = true,
                    PlaybackUrl = "https://www.amazon.com/Avengers-Infinity-Robert-Downey-Jr/dp/B07CKK1LT3" },

                new Movie() {
                    Title = "Parasite",
                    ReleaseYear = 2019,
                    Runtime = 132,
                    Plot = "A unique and unsettling story of greed and class discrimination",
                    Language = "Korean",
                    ImageUrl = "https://glide-media-cloud.s3-accelerate.amazonaws.com/2020/02/parasite-1.jpg",
                    DirectorId = 1,
                    Studio = "CJ Entertainment",
                    EditorId = null,
                    YoutubeTrailerId = "5xH0HfJHsaY",
                    ImdbId = "tt6751668",
                    IsDeleted = false,
                    IsPublic = true,
                    PlaybackUrl = "https://www.amazon.com/Parasite-English-Subtitled-Kang-Song/dp/B07YM14FRG" },

                new Movie() {
                    Title = "1917",
                    ReleaseYear = 2019,
                    Runtime = 119,
                    Plot = "A masterful new dimension to the war epic",
                    Language = "English",
                    ImageUrl = "https://rafiewsteste.files.wordpress.com/2020/02/1917.jpg",
                    DirectorId = 2,
                    Studio = "Universal Pictures",
                    EditorId = null,
                    YoutubeTrailerId = "gZjQROMAh_s",
                    ImdbId = "tt8579674",
                    IsDeleted = false,
                    IsPublic = true,
                    PlaybackUrl = "https://www.netflix.com/title/81140931" },
            });

            data.SaveChanges();
        }

        private static void SeedMovieActors(IServiceProvider serviceProvider)
        {
            var data = serviceProvider.GetRequiredService<MoviesRecommendationDbContext>();

            if (data.MovieActors.Any())
            {
                return;
            }

            data.MovieActors.AddRange(new[]
            {
                new MovieActor { MovieId = 1, ActorId = 1},
                new MovieActor { MovieId = 1, ActorId = 2},
                new MovieActor { MovieId = 1, ActorId = 3},
                new MovieActor { MovieId = 2, ActorId = 1},
                new MovieActor { MovieId = 2, ActorId = 2},
                new MovieActor { MovieId = 2, ActorId = 3},
                new MovieActor { MovieId = 3, ActorId = 1},
                new MovieActor { MovieId = 3, ActorId = 2},
                new MovieActor { MovieId = 3, ActorId = 3},
                new MovieActor { MovieId = 4, ActorId = 1},
                new MovieActor { MovieId = 4, ActorId = 2},
                new MovieActor { MovieId = 4, ActorId = 3},
                new MovieActor { MovieId = 5, ActorId = 1},
                new MovieActor { MovieId = 5, ActorId = 2},
                new MovieActor { MovieId = 5, ActorId = 3},
                new MovieActor { MovieId = 6, ActorId = 1},
                new MovieActor { MovieId = 6, ActorId = 2},
                new MovieActor { MovieId = 6, ActorId = 3},
                new MovieActor { MovieId = 7, ActorId = 1},
                new MovieActor { MovieId = 7, ActorId = 2},
                new MovieActor { MovieId = 7, ActorId = 3},
                new MovieActor { MovieId = 8, ActorId = 1},
                new MovieActor { MovieId = 8, ActorId = 2},
                new MovieActor { MovieId = 8, ActorId = 3},
            });

            data.SaveChanges();
        }

        private static void SeedMovieGenres(IServiceProvider serviceProvider)
        {
            var data = serviceProvider.GetRequiredService<MoviesRecommendationDbContext>();

            if (data.MovieGenres.Any())
            {
                return;
            }

            data.MovieGenres.AddRange(new[]
            {
                new MovieGenre { MovieId = 1, GenreId = 4 },
                new MovieGenre { MovieId = 1, GenreId = 5 },
                new MovieGenre { MovieId = 2, GenreId = 7 },
                new MovieGenre { MovieId = 2, GenreId = 1 },
                new MovieGenre { MovieId = 3, GenreId = 12 },
                new MovieGenre { MovieId = 3, GenreId = 9 },
                new MovieGenre { MovieId = 3, GenreId = 15 },
                new MovieGenre { MovieId = 4, GenreId = 7 },
                new MovieGenre { MovieId = 5, GenreId = 1 },
                new MovieGenre { MovieId = 5, GenreId = 5 },
                new MovieGenre { MovieId = 5, GenreId = 8 },
                new MovieGenre { MovieId = 6, GenreId = 4 },
                new MovieGenre { MovieId = 6, GenreId = 14 },
                new MovieGenre { MovieId = 7, GenreId = 14 },
                new MovieGenre { MovieId = 7, GenreId = 4 },
                new MovieGenre { MovieId = 7, GenreId = 2 },
                new MovieGenre { MovieId = 8, GenreId = 9 },
                new MovieGenre { MovieId = 8, GenreId = 1 },
                new MovieGenre { MovieId = 8, GenreId = 3 },
            });

            data.SaveChanges();
        }

        private static void SeedAdminEditor(IServiceProvider serviceProvider)
        {
            var data = serviceProvider.GetRequiredService<MoviesRecommendationDbContext>();

            const string adminName = "Admin";
            const string birthdate = "01/04/1996";

            var adminId = data.
                Users
                .FirstOrDefault(u => u.Email == AdminEmail)
                .Id;

            if (data.Editors.Any(e => e.UserId == adminId))
            {
                return;
            }

            data.Editors.Add(new Editor
            {
                UserId = adminId,
                FirstName = adminName,
                LastName = adminName,
                BirthDate = DateTime.Parse(birthdate),
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

                    const string adminPassword = "adminkey";

                    var user = new User
                    {
                        Email = AdminEmail,
                        UserName = AdminEmail,
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
