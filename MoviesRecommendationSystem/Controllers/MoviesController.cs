namespace MoviesRecommendationSystem.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using AutoMapper;

    using MoviesRecommendationSystem.Infrastructure;
    using MoviesRecommendationSystem.Models.Movies;
    using MoviesRecommendationSystem.Services.Editors;
    using MoviesRecommendationSystem.Services.Movies;
    using MoviesRecommendationSystem.Services.Movies.Models;
    using MoviesRecommendationSystem.Services.Reviews;
    using MoviesRecommendationSystem.Services.Watchlists;

    using static WebConstants;
    using static Common.ControllerConstants.Movies;

    public class MoviesController : Controller
    {
        private readonly IMovieService movieService;
        private readonly IEditorService editorService;
        private readonly IWatchlistService watchlistService;
        private readonly IReviewService reviewService;
        private readonly IMapper mapper;

        public MoviesController(
            IMovieService moviesService,
            IEditorService editorsService,
            IWatchlistService watchlistService,
            IReviewService reviewService,
            IMapper mapper)
        {
            this.movieService = moviesService;
            this.editorService = editorsService;
            this.watchlistService = watchlistService;
            this.reviewService = reviewService;
            this.mapper = mapper;
        }

        [Authorize]
        public IActionResult Add()
        {
            if (!this.editorService.UserIsEditor(User.GetId()) && !User.IsAdmin())
            {
                return RedirectToAction(nameof(EditorsController.Become), EditorsControllerName);
            }

            var genres = this.movieService.AllGenres();

            this.PrepareViewBagGenres(genres);

            return View(new MovieFormModel());
        }

        [HttpPost]
        [Authorize]
        public IActionResult Add(MovieFormModel movie)
        {
            var editorId = this.editorService.IdByUser(User.GetId());

            if (editorId == 0 && !User.IsAdmin())
            {
                return BadRequest();
            }

            foreach (var genreId in movie.GenreIds)
                {
                    if (!this.movieService.GenreExists(int.Parse(genreId)))
                    {
                        ModelState.AddModelError(nameof(genreId), GenreDoesNotExistMessage);
                    }
                }

            if (!ModelState.IsValid)
            {
                var genres = this.movieService.AllGenres();

                this.PrepareViewBagGenres(genres);

                return View(movie);
            }

            var movieId = this.movieService.Create(
                movie.Title,
                (int)movie.ReleaseYear,
                (int)movie.Runtime,
                movie.Plot,
                movie.Language,
                movie.ImageUrl,
                movie.PlaybackUrl,
                movie.YoutubeTrailerId,
                movie.ImdbId,
                movie.DirectorName,
                movie.Studio,
                movie.StarringActors,
                movie.GenreIds,
                editorId,
                User.IsAdmin());

            TempData[GlobalMessageKey] = User.IsAdmin() ?
                MovieAddedPublicMessage
                : MovieAddedAwaitingApprovalMessage;

            return RedirectToAction(nameof(Details), new { id = movieId, info = movie.GetInfo() });
        }

        [Authorize]
        public IActionResult Edit(int id)
        {
            var userId = User.GetId();

            if (!this.editorService.UserIsEditor(userId) && !User.IsAdmin())
            {
                return RedirectToAction(nameof(EditorsController.Become), EditorsControllerName);
            }

            var movieData = this.movieService.FormDetails(id);

            if (movieData.UserId != userId && !User.IsAdmin())
            {
                return Unauthorized();
            }

            var genres = this.movieService.AllGenres();
            var selectedGenreIds = this.movieService.SelectedGenreIds(id);

            this.PrepareViewBagGenres(genres, selectedGenreIds);

            var movieForm = this.mapper.Map<MovieFormModel>(movieData);

            movieForm.GenreIds = selectedGenreIds.ToList();

            return View(movieForm);
        }

        [Authorize]
        [HttpPost]
        public IActionResult Edit(int id, MovieFormModel movie)
        {
            var editorId = this.editorService.IdByUser(User.GetId());

            if (editorId == 0 && !User.IsAdmin())
            {
                return RedirectToAction(nameof(EditorsController.Become), EditorsControllerName);
            }

            foreach (var genreId in movie.GenreIds)
            {
                if (!this.movieService.GenreExists(int.Parse(genreId)))
                {
                    ModelState.AddModelError(nameof(genreId), GenreDoesNotExistMessage);
                }
            }

            if (!ModelState.IsValid)
            {
                var genres = this.movieService.AllGenres();

                this.PrepareViewBagGenres(genres);

                return View(movie);
            }

            if (!this.movieService.IsByEditor(id, editorId) && !User.IsAdmin())
            {
                return BadRequest();
            }

            this.movieService.Edit(
                id,
                movie.Title,
                (int)movie.ReleaseYear,
                (int)movie.Runtime,
                movie.Plot,
                movie.Language,
                movie.ImageUrl,
                movie.PlaybackUrl,
                movie.YoutubeTrailerId,
                movie.ImdbId,
                movie.DirectorName,
                movie.Studio,
                movie.StarringActors,
                movie.GenreIds,
                User.IsAdmin());

            TempData[GlobalMessageKey] = User.IsAdmin() ?
                MovieEditedPublicMessage
                : MovieEditedAwaitingApprovalMessage;

            return RedirectToAction(nameof(Details), new { id, info = movie.GetInfo() });
        }

        [Authorize]
        public IActionResult Delete(int id)
        {
            var editorId = this.editorService.IdByUser(User.GetId());

            if (editorId == 0 && !User.IsAdmin())
            {
                return BadRequest();
            }

            if (!this.movieService.IsByEditor(id, editorId) && !User.IsAdmin())
            {
                return BadRequest();
            }

            this.movieService.Delete(id);

            this.watchlistService.RemoveForAllUsers(id);

            return !User.IsAdmin() ?
                RedirectToAction(nameof(EditorContributions))
                : Redirect(AdminPanelRoute);
        }

        public IActionResult All([FromQuery] AllMoviesQueryModel query)
        {
            var queryResult = this.movieService.All(
                query.SelectedGenre,
                query.SearchTerm,
                query.Sorting,
                query.CurrentPage,
                AllMoviesQueryModel.MoviesPerPage);

            query.Genres = this.movieService
                .AllGenres()
                .Select(g => g.Name);

            query.TotalMoviesCount = queryResult.TotalMovies;
            query.Movies = queryResult.Movies;

            return View(query);
        }

        [Authorize]
        public IActionResult EditorContributions()
        {
            if (!editorService.UserIsApprovedEditor(User.GetId()) && !User.IsAdmin())
            {
                return RedirectToAction(nameof(EditorsController.Become), EditorsControllerName);
            }

            var contributions = this.movieService
                .ByUser(User.GetId());

            return View(contributions);
        }

        public IActionResult Details(int id, string info)
        {
            var movie = this.movieService
                .Details(id);

            if (info != movie.GetInfo())
            {
                return BadRequest();
            }

            movie.Reviews = this.reviewService.ReviewsForMovie(id);

            if (User.Identity.IsAuthenticated)
            {
                var userId = User.GetId();

                ViewBag.MovieIsInWatchlist = this.watchlistService.Exists(userId, id);
                ViewBag.WatchlistCount = this.watchlistService.Count(userId);
            }           

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

            ViewBag.Genres = viewBagGenres;
        }
    }
}
