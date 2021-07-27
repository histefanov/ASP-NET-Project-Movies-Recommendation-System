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

            this.AddGenres(genres, movieId);

            return movieId;
        }

        public bool Edit(
            int movieId,
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
            var movieData = this.data.Movies.Find(movieId);

            if (movieData == null)
            {
                return false;
            }

            movieData.Title = title;
            movieData.ReleaseYear = releaseYear;
            movieData.Runtime = runTime;
            movieData.Plot = plot;
            movieData.Language = language;
            movieData.ImageUrl = imageUrl;
            movieData.DirectorId = this.AddDirector(director);
            movieData.Studio = studio;
            movieData.EditorId = editorId;

            this.data.SaveChanges();

            var movieActors = this.data
              .MovieActors
              .Where(ma => ma.MovieId == movieId)
              .ToArray();

            this.data.MovieActors.RemoveRange(movieActors);
            this.data.SaveChanges();

            this.AddActors(actors, movieId);

            var movieGenres = this.data
              .MovieGenres
              .Where(mg => mg.MovieId == movieId)
              .ToArray();

            this.data.MovieGenres.RemoveRange(movieGenres);
            this.data.SaveChanges();

            this.AddGenres(genres, movieId);

            return true;
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
                    DirectorName = m.Director.Name,
                    StarringActors = ActorsToString(id),
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

        public bool IsByEditor(int movieId, int editorId)
            => this.data
                .Movies
                .Any(m => m.Id == movieId && m.EditorId == editorId);

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

        private int AddDirector(string directorName)
        {
            var director = this.data
                .Directors
                .FirstOrDefault(d => d.Name == directorName);

            if (director == null)
            {
                director = new Director { Name = directorName };

                data.Directors.Add(director);
                data.SaveChanges();
            }

            return director.Id;
        }

        private void AddGenres(IEnumerable<string> genres, int movieId)
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
            var actorsNames = actors
                .Split(',', StringSplitOptions.RemoveEmptyEntries)
                .Select(a => a.Trim());

            foreach (var actorName in actorsNames)
            {
                var actor = this.data
                    .Actors
                    .FirstOrDefault(a => a.Name == actorName);

                if (actor == null)
                {
                    actor = new Actor { Name = actorName };

                    this.data.Actors.Add(actor);
                    this.data.SaveChanges();
                }

                this.data
                    .MovieActors
                    .Add(new MovieActor
                    {
                        MovieId = movieId,
                        ActorId = actor.Id
                    });

                this.data.SaveChanges();
            }
        }

        private string ActorsToString(int movieId)
        {
            var actors = this.data
                .MovieActors
                .Where(mg => mg.MovieId == movieId)
                .Select(mg => mg.Actor.Name)
                .ToList();

            return string.Join(", ", actors);
        }
    }
}
