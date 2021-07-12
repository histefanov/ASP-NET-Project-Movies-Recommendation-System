namespace MoviesRecommendationSystem.Data
{
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using MoviesRecommendationSystem.Data.Models;

    public class MoviesRecommendationDbContext : IdentityDbContext
    {
        public MoviesRecommendationDbContext(DbContextOptions<MoviesRecommendationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Actor> Actors { get; init; }

        public DbSet<Director> Directors { get; init; }

        public DbSet<Genre> Genres { get; init; }

        public DbSet<Movie> Movies { get; init; }

        public DbSet<MovieActor> MovieActors { get; init; }

        public DbSet<MovieGenre> MovieGenres { get; init; }

        public DbSet<Series> Series { get; init; }

        public DbSet<SeriesActor> SeriesActors { get; init; }

        public DbSet<SeriesGenre> SeriesGenres { get; init; }

        public DbSet<Studio> Studios { get; init; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Movie>()
                .HasOne(x => x.Director)
                .WithMany(x => x.Movies)
                .HasForeignKey(x => x.DirectorId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Movie>()
                .HasOne(x => x.Studio)
                .WithMany(x => x.Movies)
                .HasForeignKey(x => x.StudioId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Series>()
                .HasOne(x => x.Studio)
                .WithMany(x => x.Series)
                .HasForeignKey(x => x.StudioId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<MovieActor>()
                .HasKey(x => new { x.MovieId, x.ActorId });

            builder.Entity<MovieGenre>()
                .HasKey(x => new { x.MovieId, x.GenreId });

            builder.Entity<SeriesActor>()
                .HasKey(x => new { x.SeriesId, x.ActorId });

            builder.Entity<SeriesGenre>()
                .HasKey(x => new { x.SeriesId, x.GenreId });

            base.OnModelCreating(builder);
        }
    }
}
