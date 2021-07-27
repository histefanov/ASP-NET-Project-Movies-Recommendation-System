namespace MoviesRecommendationSystem.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using MoviesRecommendationSystem.Infrastructure;
    using MoviesRecommendationSystem.Models.Movies;
    using MoviesRecommendationSystem.Services.Editors;
    using MoviesRecommendationSystem.Services.Movies;
    using MoviesRecommendationSystem.Services.Movies.Models;

    public class MoviesController : Controller
    {
        private readonly IMoviesService moviesService;
        private readonly IEditorsService editorsService;

        public MoviesController(
            IMoviesService moviesService,
            IEditorsService editorsService)
        {
            this.moviesService = moviesService;
            this.editorsService = editorsService;
        }

        [Authorize]
        public IActionResult Add()
        {
            if (!this.editorsService.UserIsEditor(this.User.GetId()))
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }

            var genres = this.moviesService.AllGenres();

            this.PrepareViewBagGenres(genres);

            return View(new MovieFormModel());
        }

        [HttpPost]
        [Authorize]
        public IActionResult Add(MovieFormModel movie)
        {
            var editorId = this.editorsService.GetIdByUser(User.GetId());

            if (editorId == 0)
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }

            //TODO: Limit the number of genres a movie can have

            foreach (var genreId in movie.GenreIds)
            {
                if (!this.moviesService.GenreExists(int.Parse(genreId)))
                {
                    this.ModelState.AddModelError(nameof(genreId), $"Genre '{genreId}' does not exist.");
                }
            }

            if (!ModelState.IsValid)
            {
                var genres = this.moviesService.AllGenres();

                this.PrepareViewBagGenres(genres);

                return View(movie);
            }

            this.moviesService.Create(
                movie.Title,
                (int)movie.ReleaseYear,
                (int)movie.Runtime,
                movie.Plot,
                movie.Language,
                movie.ImageUrl,
                movie.Director,
                movie.Studio,
                movie.StarringActors,
                movie.GenreIds,
                editorId);

            return RedirectToAction("Index", "Home");
        }

        [Authorize]
        public IActionResult Edit(int id)
        {
            var userId = this.User.GetId();

            if (!this.editorsService.UserIsEditor(userId))
            {
                return RedirectToAction(nameof(EditorsController.Create), "EditorsController");
            }

            var movie = this.moviesService.Details(id);

            if (movie.UserId != userId)
            {
                return Unauthorized();
            }

            var genres = this.moviesService.AllGenres();
            var selectedGenreIds = this.moviesService.SelectedGenreIds(id);

            this.PrepareViewBagGenres(genres, selectedGenreIds);

            return View(new MovieFormModel
            {
                Title = movie.Title,
                ReleaseYear = movie.ReleaseYear,
                Runtime = movie.Runtime,
                Plot = movie.Plot,
                Language = movie.Language,
                ImageUrl = movie.ImageUrl,
                Studio = movie.Studio,
                Director = movie.DirectorName,
                StarringActors = movie.StarringActors
            });
        }

        [HttpPost]
        [Authorize]
        public IActionResult Edit(int id, MovieFormModel movie)
        {
            var editorId = this.editorsService.GetIdByUser(User.GetId());

            if (editorId == 0)
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }

            foreach (var genreId in movie.GenreIds)
            {
                if (!this.moviesService.GenreExists(int.Parse(genreId)))
                {
                    this.ModelState.AddModelError(nameof(genreId), $"Genre '{genreId}' does not exist.");
                }
            }

            if (!ModelState.IsValid)
            {
                var genres = this.moviesService.AllGenres();

                this.PrepareViewBagGenres(genres);

                return View(movie);
            }

            if (!this.moviesService.IsByEditor(id, editorId))
            {
                return BadRequest();
            }

            this.moviesService.Edit(
                id,
                movie.Title,
                (int)movie.ReleaseYear,
                (int)movie.Runtime,
                movie.Plot,
                movie.Language,
                movie.ImageUrl,
                movie.Director,
                movie.Studio,
                movie.StarringActors,
                movie.GenreIds,
                editorId);

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

        [Authorize]
        public IActionResult EditorContributions()
        {
            var contributions = this.moviesService
                .ByUser(this.User.GetId());

            return View(contributions);
        }

        private void PrepareViewBagGenres(
            IEnumerable<MovieGenreServiceModel> genres,
            IEnumerable<string> selectedGenreIds = null)
        {
            var viewBagGenres = new List<SelectListItem>();

            foreach (var genre in genres)
            {
                viewBagGenres.Add(new SelectListItem
                {
                    Text = genre.Name,
                    Value = genre.Id.ToString()
                });
            }

            if (selectedGenreIds != null)
            {
                foreach (var genre in viewBagGenres)
                {
                    genre.Selected = selectedGenreIds.Contains(genre.Value);
                }
            }

            this.ViewBag.Genres = viewBagGenres;
        }
    }
}
