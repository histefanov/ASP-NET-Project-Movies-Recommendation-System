namespace MoviesRecommendationSystem.Services.Editors
{
    using System.Linq;
    using MoviesRecommendationSystem.Data;

    public class EditorsService : IEditorsService
    {
        private readonly MoviesRecommendationDbContext data;

        public EditorsService(MoviesRecommendationDbContext data) 
            => this.data = data;

        public bool UserIsEditor(string userId)
            => this.data
                   .Editors
                   .Any(e => e.UserId == userId);

        public int IdByUser(string userId)
            => this.data
                .Editors
                .Where(e => e.UserId == userId)
                .Select(e => e.Id)
                .FirstOrDefault();
    }
}
