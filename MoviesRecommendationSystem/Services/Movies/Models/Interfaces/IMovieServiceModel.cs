namespace MoviesRecommendationSystem.Services.Movies.Models.Interfaces
{
    public interface IMovieServiceModel
    {
        string Title { get; }

        int? ReleaseYear { get; }
    }
}
