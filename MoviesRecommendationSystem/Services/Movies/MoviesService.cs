namespace MoviesRecommendationSystem.Services.Movies
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using MoviesRecommendationSystem.Data;
    using MoviesRecommendationSystem.Data.Models;
    using MoviesRecommendationSystem.Models.Enums;
    using MoviesRecommendationSystem.Services.Movies.Models;

    public class MoviesService : IMoviesService
    {
        private readonly MoviesRecommendationDbContext data;

        public MoviesService(MoviesRecommendationDbContext data) 
            => this.data = data;

        //TODO: try to pass the arguments in a better way
        public int Create(
            string title, 
            int releaseYear, 
            int runTime, 
            string plot, 
            string language, 
            string imageUrl, 
            string director, 
            string studio,
            string actors,
            IEnumerable<string> genres,
            int editorId)
        {
            var movieData = new Movie
            {
                Title = title,
                ReleaseYear = releaseYear,
                Runtime = runTime,
                Plot = plot,
                Language = language,
                ImageUrl = imageUrl,
                DirectorId = this.AddDirector(director),
                Studio = studio,
                EditorId = editorId
            };

            this.data.Movies.Add(movieData);
            this.data.SaveChanges();

            var movieId = movieData.Id;

            this.AddActors(actors, movieId);

            this.AddMovieGenres(genres, movieId);

            return movieId;
        }

        public MovieQueryServiceModel All(
            string selectedGenre,
            string searchTerm,
            MovieSorting sorting,
            int currentPage,
            int moviesPerPage)
        {
            var moviesQuery = this.data.Movies.AsQueryable();

            if (!string.IsNullOrWhiteSpace(selectedGenre))
            {
                var genreId = this.data
                    .Genres
                    .FirstOrDefault(g => g.Name == selectedGenre)
                    .Id;

                moviesQuery = moviesQuery
                    .Where(m => m.MovieGenres.Any(mg => mg.GenreId == genreId));
            }

            moviesQuery = sorting switch
            {
                MovieSorting.DateCreatedDescending => moviesQuery.OrderByDescending(m => m.ReleaseYear),
                MovieSorting.DateCreatedAscending => moviesQuery.OrderBy(m => m.ReleaseYear),
                MovieSorting.DateCreated or _ => moviesQuery.OrderByDescending(m => m.Id),
            };

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                //TODO: Improve code quality by replacing .ToLower() with another functionality
                moviesQuery = moviesQuery.Where(m =>
                    m.Title.ToLower().Contains(searchTerm.ToLower()));
            }

            var totalMoviesCount = moviesQuery.Count();

            var movies = GetMovies(
                 moviesQuery
                    .Skip((currentPage - 1) * moviesPerPage)
                    .Take(moviesPerPage));

            return new MovieQueryServiceModel
            {
                TotalMovies = totalMoviesCount,
                CurrentPage = currentPage,
                MoviesPerPage = moviesPerPage,
                Movies = movies
            };
        }
        public MovieDetailsServiceModel Details(int id)
            => this.data
                .Movies
                .Where(m => m.Id == id)
                .Select(m => new MovieDetailsServiceModel
                {
                    Id = m.Id,
                    Title = m.Title,
                    ReleaseYear = m.ReleaseYear,
                    Runtime = m.Runtime,
                    Plot = m.Plot,
                    Language = m.Language,
                    ImageUrl = m.ImageUrl,
                    Studio = m.Studio,
                    DirectorId = m.DirectorId,
                    //StarringActors = ActorsToString(id),
                    Genres = m.MovieGenres
                                .Select(mg => mg.Genre.Name)
                                .ToList(),
                    EditorId = (int)m.EditorId,
                    EditorName = m.Editor.FirstName,
                    UserId = m.Editor.UserId
                })
                .FirstOrDefault();

        public IEnumerable<MovieGenreServiceModel> AllGenres()
            => this.data
                .Genres
                .Select(g => new MovieGenreServiceModel
                {
                    Id = g.Id,
                    Name = g.Name
                })
                .OrderBy(g => g.Name)
                .ToList();

        public IEnumerable<string> SelectedGenreIds(int movieId)
            => this.data
                .MovieGenres
                .Where(mg => mg.MovieId == movieId)
                .Select(mg => mg.GenreId.ToString())
                .ToList();

        public IEnumerable<MovieServiceModel> ByUser(string userId)
            => GetMovies(this.data
                .Movies
                .Where(m => m.Editor.UserId == userId));
        
        public bool GenreExists(int id)
            => this.data
                .Genres
                .Any(g => g.Id == id);

        private static IEnumerable<MovieServiceModel> GetMovies(IQueryable<Movie> movieQuery)
            => movieQuery
                .Select(m => new MovieServiceModel
                {
                    Id = m.Id,
                    Title = m.Title,
                    ReleaseYear = m.ReleaseYear,
                    Plot = m.Plot,
                    ImageUrl = m.ImageUrl,
                    Genres = m.MovieGenres
                                .Select(mg => mg.Genre.Name)
                                .ToList()
                })
                .ToList();

        private int AddDirector(string director)
        {
            var directorNameParts = director.Split();
            var directorFirstName = directorNameParts[0];
            var directorLastName = directorNameParts[1];

            if (!this.data.Directors.Any(d => d.FirstName == directorFirstName && d.LastName == directorLastName))
            {
                data.Directors.Add(new Director
                {
                    FirstName = directorFirstName,
                    LastName = directorLastName
                });

                data.SaveChanges();
            }

            var directorId = this.data
                .Directors
                .FirstOrDefault(d => d.FirstName == directorFirstName && d.LastName == directorLastName)
                .Id;

            return directorId;
        }

        private void AddMovieGenres(IEnumerable<string> genres, int movieId)
        {
            foreach (var genreId in genres)
            {
                this.data
                    .MovieGenres
                    .Add(new MovieGenre
                    {
                        MovieId = movieId,
                        GenreId = int.Parse(genreId)
                    });
            }

            data.SaveChanges();
        }

        private void AddActors(string actors, int movieId)
        {
            var actorsNames = actors.Split(',', StringSplitOptions.RemoveEmptyEntries);

            foreach (var actorName in actorsNames)
            {
                var actor = this.data
                    .Actors
                    .FirstOrDefault(a => a.Name == actorName);

                if (actor == null)
                {
                    actor = new Actor { Name = actorName };

                    this.data.SaveChanges();
                }

                var actorId = actor.Id;

                this.data
                    .MovieActors
                    .Add(new MovieActor
                    {
                        MovieId = movieId,
                        ActorId = actorId
                    });

                this.data.SaveChanges();
            }                                     
        }
    }
}
