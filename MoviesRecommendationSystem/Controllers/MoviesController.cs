namespace MoviesRecommendationSystem.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AutoMapper;
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
        private readonly IMapper mapper;

        public MoviesController(
            IMoviesService moviesService,
            IEditorsService editorsService,
            IMapper mapper)
        {
            this.moviesService = moviesService;
            this.editorsService = editorsService;
            this.mapper = mapper;
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
            var editorId = this.editorsService.IdByUser(User.GetId());

            if (editorId == 0)
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }

            //TODO: Limit the number of genres a movie can have and fix the NullReferenceException for the null genre collection

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
                movie.DirectorName,
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

            if (!this.editorsService.UserIsEditor(userId) && !this.User.IsAdmin())
            {
                return RedirectToAction(nameof(EditorsController.Become), "Editors");
            }

            var movie = this.moviesService.Details(id);

            if (movie.UserId != userId && !this.User.IsAdmin())
            {
                return Unauthorized();
            }

            var genres = this.moviesService.AllGenres();
            var selectedGenreIds = this.moviesService.SelectedGenreIds(id);

            this.PrepareViewBagGenres(genres, selectedGenreIds);

            var movieForm = this.mapper.Map<MovieFormModel>(movie);

            return View(movieForm);
        }

        [HttpPost]
        [Authorize]
        public IActionResult Edit(int id, MovieFormModel movie)
        {
            var editorId = this.editorsService.IdByUser(User.GetId());

            if (editorId == 0 && !this.User.IsAdmin())
            {
                return RedirectToAction(nameof(EditorsController.Become), "Editors");
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

            if (!this.moviesService.IsByEditor(id, editorId) && !this.User.IsAdmin())
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
                movie.DirectorName,
                movie.Studio,
                movie.YoutubeTrailerId,
                movie.StarringActors,
                movie.GenreIds);

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

        public IActionResult Details(int id)
        {
            var movie = this.moviesService
                .Details(id);

            return View(movie);
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
