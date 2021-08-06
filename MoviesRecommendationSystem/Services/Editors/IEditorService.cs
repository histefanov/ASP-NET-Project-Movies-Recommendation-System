namespace MoviesRecommendationSystem.Services.Editors
{
    using System;

    public interface IEditorService
    {
        int Create(
            string firstName,
            string lastName,
            DateTime birthDate,
            string userId); 

        bool UserIsEditor(string userId);

        int IdByUser(string userId);
    }
}
