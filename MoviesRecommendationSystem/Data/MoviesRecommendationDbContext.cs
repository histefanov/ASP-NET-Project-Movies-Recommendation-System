namespace MoviesRecommendationSystem.Data
{
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using MoviesRecommendationSystem.Data.Models;

    public class MoviesRecommendationDbContext : IdentityDbContext<User>
    {
        public MoviesRecommendationDbContext(DbContextOptions<MoviesRecommendationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Actor> Actors { get; init; }

        public DbSet<Director> Directors { get; init; }

        public DbSet<Editor> Editors { get; init; }

        public DbSet<Genre> Genres { get; init; }

        public DbSet<Movie> Movies { get; init; }

        public DbSet<MovieActor> MovieActors { get; init; }

        public DbSet<MovieGenre> MovieGenres { get; init; }

        public DbSet<UserWatchlistMovie> UserWatchlistMovies { get; init; }

        public DbSet<Review> Reviews { get; init; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder
                .Entity<Editor>()
                .HasOne<User>()
                .WithOne()
                .HasForeignKey<Editor>(e => e.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Movie>()
                .HasOne(m => m.Editor)
                .WithMany(e => e.Movies)
                .HasForeignKey(m => m.EditorId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Movie>()
                .HasOne(x => x.Director)
                .WithMany(x => x.Movies)
                .HasForeignKey(x => x.DirectorId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<MovieActor>()
                .HasKey(x => new { x.MovieId, x.ActorId });

            builder.Entity<MovieGenre>()
                .HasKey(x => new { x.MovieId, x.GenreId });

            builder.Entity<UserWatchlistMovie>()
                .HasKey(x => new { x.UserId, x.MovieId });

            builder.Entity<Review>()
                .HasOne(r => r.User)
                .WithMany(u => u.Reviews)
                .HasForeignKey(r => r.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Review>()
                .HasOne(r => r.Movie)
                .WithMany(u => u.Reviews)
                .HasForeignKey(r => r.MovieId)
                .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(builder);
        }
    }
}
