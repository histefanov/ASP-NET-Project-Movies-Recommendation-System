namespace MoviesRecommendationSystem.Services.Editors
{
    using System;
    using System.Linq;
    using MoviesRecommendationSystem.Data;
    using MoviesRecommendationSystem.Data.Models;

    public class EditorService : IEditorService
    {
        private readonly MoviesRecommendationDbContext data;

        public EditorService(MoviesRecommendationDbContext data) 
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

        public int Create(
            string firstName,
            string lastName,
            DateTime birthDate,
            string userId)
        {
            var editorData = new Editor
            {
                FirstName =firstName,
                LastName = lastName,
                BirthDate = birthDate,
                UserId = userId
            };

            this.data.Editors.Add(editorData);

            this.data.SaveChanges();

            return editorData.Id;
        }
    }
}
