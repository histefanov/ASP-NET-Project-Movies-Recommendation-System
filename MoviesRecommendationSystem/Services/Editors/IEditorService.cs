namespace MoviesRecommendationSystem.Services.Editors
{
    using MoviesRecommendationSystem.Services.Editors.Models;
    using System;
    using System.Collections.Generic;

    public interface IEditorService
    {
        int Create(
            string firstName,
            string lastName,
            DateTime birthDate,
            string userId); 

        bool UserIsEditor(string userId);

        bool UserIsApprovedEditor(string userId);

        int IdByUser(string userId);

        void SwitchApprovalStatus(int id);

        IEnumerable<EditorServiceModel> All();
    }
}
