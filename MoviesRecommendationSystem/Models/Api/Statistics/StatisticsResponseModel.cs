namespace MoviesRecommendationSystem.Models.Api.Statistics
{
    public class StatisticsResponseModel
    {
        public int TotalMovies { get; init; }
        
        public int TotalGenres { get; init; }

        public int TotalActors { get; init; }

        public int TotalDirectors { get; init; }
    }
}
