namespace MoviesRecommendationSystem.Services.Editors
{
    public interface IEditorsService
    {
        bool UserIsEditor(string userId);

        int IdByUser(string userId);
    }
}
