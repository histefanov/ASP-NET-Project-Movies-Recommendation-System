namespace MoviesRecommendationSystem.Services.Editors
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using MoviesRecommendationSystem.Data;
    using MoviesRecommendationSystem.Data.Models;
    using MoviesRecommendationSystem.Services.Editors.Models;

    public class EditorService : IEditorService
    {
        private readonly MoviesRecommendationDbContext data;
        private readonly IMapper mapper;

        public EditorService(MoviesRecommendationDbContext data, IMapper mapper)
        {
            this.data = data;
            this.mapper = mapper;
        }

        public bool UserIsEditor(string userId)
            => this.data
                .Editors
                .Any(e => e.UserId == userId);

        public bool UserIsApprovedEditor(string userId)
            => this.data
                .Editors
                .Any(e => e.UserId == userId && e.IsApproved);

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

        public void SwitchApprovalStatus(int id)
        {
            var editor = this.data
                .Editors
                .Find(id);

            editor.IsApproved = !editor.IsApproved;

            this.data.SaveChanges();
        }

        public IEnumerable<EditorServiceModel> All()
        {
            var editors = this.data
                .Editors
                .ProjectTo<EditorServiceModel>(this.mapper.ConfigurationProvider)
                .ToList();

            foreach (var editor in editors)
            {
                editor.Email = this.data
                    .Users
                    .FirstOrDefault(u => u.Id == editor.UserId)
                    .Email;
            }

            return editors;
        }
    }
}
