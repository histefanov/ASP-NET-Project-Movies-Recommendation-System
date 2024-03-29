﻿namespace MoviesRecommendationSystem.Infrastructure
{
    using System.Linq;
    using AutoMapper;

    using MoviesRecommendationSystem.Data.Models;
    using MoviesRecommendationSystem.Models.Movies;
    using MoviesRecommendationSystem.Services.Editors.Models;
    using MoviesRecommendationSystem.Services.Movies.Models;
    using MoviesRecommendationSystem.Services.Reviews.Reviews;
    using MoviesRecommendationSystem.Services.Watchlists.Models;

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            this.CreateMap<Movie, MovieServiceModel>()
                .ForMember(fm => fm.Genres, cfg => cfg.MapFrom(m => m.MovieGenres
                    .Select(mg => mg.Genre.Name)
                    .ToList()));

            this.CreateMap<Movie, MovieDetailsServiceModel>()
                .ForMember(md => md.Genres, cfg => cfg.MapFrom(m => m.MovieGenres
                    .Select(mg => mg.Genre.Name)
                    .ToList()))
                .ForMember(md => md.EditorName, cfg => cfg.MapFrom(m => m.Editor.FirstName + " " + m.Editor.LastName))
                .ForMember(md => md.UserId, cfg => cfg.MapFrom(m => m.Editor.UserId));

            this.CreateMap<Movie, MovieFormDataServiceModel>()
                .ForMember(md => md.Genres, cfg => cfg.MapFrom(m => m.MovieGenres
                    .Select(mg => mg.Genre.Name)
                    .ToList()))
                .ForMember(md => md.UserId, cfg => cfg.MapFrom(m => m.Editor.UserId));

            this.CreateMap<Movie, RandomMovieServiceModel>()
                .ForMember(rm => rm.DirectorName, cfg => cfg.MapFrom(m => m.Director.Name))
                .ForMember(md => md.Genres, cfg => cfg.MapFrom(m => m.MovieGenres
                    .Select(mg => mg.Genre.Name)
                    .ToList()));

            this.CreateMap<MovieFormDataServiceModel, MovieFormModel>();

            this.CreateMap<Genre, MovieGenreServiceModel>();

            this.CreateMap<UserWatchlistMovie, WatchlistMovieServiceModel>()
                .ForMember(w => w.Id, cfg => cfg.MapFrom(m => m.MovieId))
                .ForMember(w => w.Title, cfg => cfg.MapFrom(m => m.Movie.Title))
                .ForMember(w => w.ReleaseYear, cfg => cfg.MapFrom(m => m.Movie.ReleaseYear));

            this.CreateMap<Review, ReviewServiceModel>()
                .ForMember(r => r.Author, cfg => cfg.MapFrom(r => r.User.Name))
                .ForMember(r => r.DateCreated, cfg => cfg.MapFrom(r => r.DateCreated.ToString("dd MMM yyyy, HH:mm")));

            this.CreateMap<Editor, EditorServiceModel>()
                .ForMember(e => e.BirthDate, cfg => cfg.MapFrom(e => e.BirthDate.ToString("dd/MM/yyyy")));
        }
    }
}
