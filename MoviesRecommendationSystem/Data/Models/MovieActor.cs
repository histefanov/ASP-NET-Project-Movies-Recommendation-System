namespace MoviesRecommendationSystem.Data.Models
{
    public class MovieActor
    {
        public int MovieId { get; init; }

        public Movie Movie { get; set; }

        public int ActorId { get; set; }

        public Actor Actor { get; set; }
    }
}
