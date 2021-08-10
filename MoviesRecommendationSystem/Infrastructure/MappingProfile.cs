namespace MoviesRecommendationSystem.Infrastructure
{
    using AutoMapper;
    using MoviesRecommendationSystem.Data.Models;
    using MoviesRecommendationSystem.Models.Movies;
    using MoviesRecommendationSystem.Services.Movies.Models;
    using MoviesRecommendationSystem.Services.Watchlists.Models;
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
                .ForMember(md => md.Genres, cfg => cfg.MapFrom(m => m.MovieGenres
                    .Select(mg => mg.Genre.Name)
                    .ToList()))
                .ForMember(md => md.EditorName, cfg => cfg.MapFrom(m => m.Editor.FirstName + " " + m.Editor.LastName))
                .ForMember(md => md.UserId, cfg => cfg.MapFrom(m => m.Editor.UserId));

            this.CreateMap<MovieDetailsServiceModel, MovieFormModel>();

            this.CreateMap<Genre, MovieGenreServiceModel>();

            this.CreateMap<UserWatchlistMovie, WatchlistMovieServiceModel>()
                .ForMember(w => w.Id, cfg => cfg.MapFrom(m => m.MovieId))
                .ForMember(w => w.Title, cfg => cfg.MapFrom(m => m.Movie.Title))
                .ForMember(w => w.ReleaseYear, cfg => cfg.MapFrom(m => m.Movie.ReleaseYear));
        }
    }
}
