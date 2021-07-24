namespace MoviesRecommendationSystem.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using MoviesRecommendationSystem.Data;
    using MoviesRecommendationSystem.Data.Models;
    using MoviesRecommendationSystem.Infrastructure;
    using MoviesRecommendationSystem.Models.Enums;
    using MoviesRecommendationSystem.Models.Movies;
    using MoviesRecommendationSystem.Services.Movies;

    public class MoviesController : Controller
    {
        private readonly MoviesRecommendationDbContext data;
        private readonly IMoviesService moviesService;

        public MoviesController(MoviesRecommendationDbContext data, IMoviesService moviesService)
        {
            this.data = data;
            this.moviesService = moviesService;
        }

        [Authorize]
        public IActionResult Add()
        {
            if (!this.UserIsEditor())
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }

            this.PrepareViewBagGenres();

            return View(new AddMovieFormModel());
        }

        [HttpPost]
        [Authorize]
        public IActionResult Add(AddMovieFormModel movie)
        {
            var editor = this.data
                .Editors
                .FirstOrDefault(e => e.UserId == this.User.GetId());

            if (editor == null)
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }

            if (!ModelState.IsValid)
            {
                this.PrepareViewBagGenres();

                return View(movie);
            }

            var movieData = new Movie
            {
                Title = movie.Title,
                ReleaseYear = (int)movie.ReleaseYear,
                Runtime = (int)movie.Runtime,
                Plot = movie.Plot,
                Language = movie.Language,
                ImageUrl = movie.ImageUrl,
                DirectorId = this.GetDirectorId(movie.Director),
                Studio = movie.Studio,
                EditorId = editor.Id
            };

            this.data.Movies.Add(movieData);

            this.data.SaveChanges();

            var movieId = this.data
                .Movies
                .FirstOrDefault(m => m.Title == movie.Title)
                .Id;

            this.AddActors(movie.StarringActors, movieId);

            this.AddMovieGenres(movie.Genres, movieId);

            return RedirectToAction("Index", "Home");
        }

        public IActionResult All([FromQuery] AllMoviesQueryModel query)
        {
            var queryResult = this.moviesService.All(
                query.SelectedGenre,
                query.SearchTerm,
                query.Sorting,
                query.CurrentPage,
                AllMoviesQueryModel.MoviesPerPage);

            query.Genres = this.moviesService
                .AllGenres()
                .Select(g => g.Name);

            query.TotalMoviesCount = queryResult.TotalMovies;
            query.Movies = queryResult.Movies;

            return View(query);
        }

        private void AddMovieGenres(List<string> genreIds, int movieId)
        {
            foreach (var genreId in genreIds)
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

        private void PrepareViewBagGenres()
        {
            var genres = this.GetMovieGenres();

            var viewBagGenres = new List<SelectListItem>();

            foreach (var genre in genres)
            {
                viewBagGenres.Add(new SelectListItem
                {
                    Text = genre.Name,
                    Value = genre.Id.ToString()
                });
            }

            this.ViewBag.Genres = viewBagGenres;
        }

        private void AddActors(string starringActors, int movieId)
        {
            var actorsArray = starringActors.Split(',', StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0; i < actorsArray.Length; i++)
            {
                var actor = actorsArray[i].Trim();

                var actorNames = actor.Split();

                string
                    firstName = actorNames[0],
                    middleName = null,
                    lastName;

                if (actorNames.Length == 2)
                {
                    lastName = actorNames[1];
                }
                else if (actorNames.Length == 3)
                {
                    middleName = actorNames[1];
                    lastName = actorNames[2];
                }
                else if (actorNames.Length > 3)
                {
                    lastName = actorNames[actorNames.Length - 1];
                }
                else
                {
                    continue;
                }

                if (!this.data.Actors.Any(a =>
                    a.FirstName == firstName &&
                    a.MiddleName == middleName &&
                    a.LastName == lastName))
                {
                    this.data.Actors.Add(new Actor
                    {
                        FirstName = firstName,
                        MiddleName = middleName,
                        LastName = lastName
                    });

                    this.data.SaveChanges();
                }

                var actorId = this.data
                    .Actors
                    .FirstOrDefault(a =>
                        a.FirstName == firstName &&
                        a.MiddleName == middleName &&
                        a.LastName == lastName)
                    .Id;

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

        private IEnumerable<MovieGenreViewModel> GetMovieGenres()
            => this.data.Genres
                   .Select(g => new MovieGenreViewModel
                   {
                       Id = g.Id,
                       Name = g.Name
                   })
                   .OrderBy(g => g.Name)
                   .ToList();

        private IEnumerable<string> GetGenresAsStrings()
            => this.data
                .Genres
                .Select(g => g.Name)
                .OrderBy(n => n)
                .ToList();

        private int GetDirectorId(string director)
        {
            var directorNameParts = director.Split();
            var directorFirstName = directorNameParts[0];
            var directorLastName = directorNameParts[1];

            this.AddDirector(directorFirstName, directorLastName);

            var directorId = this.data
                .Directors
                .FirstOrDefault(d => d.FirstName == directorFirstName && d.LastName == directorLastName)
                .Id;

            return directorId;
        }

        private void AddDirector(string directorFirstName, string directorLastName)
        {
            if (!this.data.Directors.Any(d => d.FirstName == directorFirstName && d.LastName == directorLastName))
            {
                data.Directors.Add(new Director
                {
                    FirstName = directorFirstName,
                    LastName = directorLastName
                });

                data.SaveChanges();
            }
        }

        private bool UserIsEditor()
            => this.data
                .Editors
                .Any(e => e.UserId == this.User.GetId());

    }
}
