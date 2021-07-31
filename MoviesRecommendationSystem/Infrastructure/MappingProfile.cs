﻿namespace MoviesRecommendationSystem.Infrastructure
{
    using AutoMapper;
    using MoviesRecommendationSystem.Data.Models;
    using MoviesRecommendationSystem.Models.Movies;
    using MoviesRecommendationSystem.Services.Movies.Models;
    using System.Linq;

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            this.CreateMap<Movie, MovieServiceModel>()
                .ForMember(fm => fm.Genres, cfg => cfg.MapFrom(m => m.MovieGenres
                    .Select(mg => mg.Genre.Name)
                    .ToList()));

            this.CreateMap<Movie, MovieDetailsServiceModel>()
                .ForMember(fm => fm.Genres, cfg => cfg.MapFrom(m => m.MovieGenres
                    .Select(mg => mg.Genre.Name)
                    .ToList()))
                .ForMember(fm => fm.EditorName, cfg => cfg.MapFrom(m => m.Editor.FirstName + " " + m.Editor.LastName))
                .ForMember(fm => fm.UserId, cfg => cfg.MapFrom(m => m.Editor.UserId));

            this.CreateMap<MovieDetailsServiceModel, MovieFormModel>();

            this.CreateMap<Genre, MovieGenreServiceModel>();
        }
    }
}