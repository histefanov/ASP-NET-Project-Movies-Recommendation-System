namespace MoviesRecommendationSystem.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.AspNetCore.Mvc;
    using MoviesRecommendationSystem.Data;
    using MoviesRecommendationSystem.Data.Models;
    using MoviesRecommendationSystem.Models.Movies;

    public class MoviesController : Controller
    {
        private readonly MoviesRecommendationDbContext data;

        public MoviesController(MoviesRecommendationDbContext data) 
            => this.data = data;

        public IActionResult Add() => View(new AddMovieFormModel
        {
            //Genres = this.GetMovieGenres()
        });

        [HttpPost]
        public IActionResult Add(AddMovieFormModel movie)
        {
            if (!ModelState.IsValid)
            {
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
                DirectorId = GetDirectorId(movie.Director),
                StudioId = GetStudioId(movie.Studio)
            };

            this.data.Movies.Add(movieData);

            this.data.SaveChanges();

            return RedirectToAction("Index", "Home");
        }

        public IActionResult All()
        {
            var movies = this.data
                .Movies
                .OrderByDescending(m => m.Id)
                .Select(m => new MovieListingViewModel
                {
                    Id = m.Id,
                    Title = m.Title,
                    ReleaseYear = m.ReleaseYear,
                    Plot = m.Plot,
                    ImageUrl = m.ImageUrl
                })
                .ToList();
            
            return View(movies);
        }

        private IEnumerable<MovieGenreViewModel> GetMovieGenres()
            => this.data.Genres
                   .Select(g => new MovieGenreViewModel
                   {
                       Id = g.Id,
                       Name = g.Name
                   })
                   .ToList();

        private int GetDirectorId(string director)
        {
            var directorNameParts = director.Split();
            var directorFirstName = directorNameParts[0];
            var directorLastName = directorNameParts[1];

            if (!this.data.Directors.Any(d => d.FirstName + " " + d.LastName == director))
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

        private int GetStudioId(string studio)
        {
            if (!this.data.Studios.Any(s => s.Name == studio))
            {
                this.data.Studios.Add(new Studio
                {
                    Name = studio
                });

                data.SaveChanges();
            }

            var studioId = this.data
                .Studios
                .FirstOrDefault(s => s.Name == studio)
                .Id;

            return studioId;
        }
    }
}
