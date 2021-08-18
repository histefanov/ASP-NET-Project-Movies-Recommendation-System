namespace MoviesRecommendationSystem.Test.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Xunit;

    using MoviesRecommendationSystem.Test.Mocks;
    using MoviesRecommendationSystem.Services.Editors;
    using MoviesRecommendationSystem.Data.Models;
    using MoviesRecommendationSystem.Services.Editors.Models;

    public class EditorServiceTest
    {
        [Fact]
        public void CreateShouldAddEditorAndReturnEditorId()
        {
            // Arrange

            var data = DataMock.Instance;
            var mapper = MapperMock.Instance;
            var editorService = new EditorService(data, mapper);

            // Act

            var result = editorService.Create(
                "Some",
                "Editor",
                DateTime.Parse("11-09-2001"),
                "someUser");

            // Assert

            Assert.IsType<int>(result);
            Assert.NotEmpty(data.Editors);
            Assert.Equal(1, data.Editors.FirstOrDefault().Id);
        }

        [Fact]
        public void UserIsEditorShouldReturnBoolResult()
        {
            // Arrange

            var data = DataMock.Instance;
            var mapper = MapperMock.Instance;
            var editorService = new EditorService(data, mapper);

            // Act

            var result = editorService.UserIsEditor("someUserId");

            // Assert

            Assert.IsType<bool>(result);
        }

        [Fact]
        public void UserIsApprovedEditorShouldReturnFalseIfIsNotApproved()
        {
            //Arrange

            var data = DataMock.Instance;
            var mapper = MapperMock.Instance;
            var editorService = new EditorService(data, mapper);

            editorService.Create(
                "Some",
                "Editor",
                DateTime.Parse("11-09-2001"),
                "someUser");

            // Act

            var result = editorService.UserIsApprovedEditor("someUserId");

            // Assert

            Assert.IsType<bool>(result);
            Assert.False(result);
        }

        [Fact]
        public void IdByUserShouldReturnEditorId()
        {
            //Arrange

            var data = DataMock.Instance;
            var mapper = MapperMock.Instance;
            var editorService = new EditorService(data, mapper);

            editorService.Create(
                "Some",
                "Editor",
                DateTime.Parse("11-09-2001"),
                "someUser");

            // Act

            var result = editorService.IdByUser("someUserId");

            // Assert

            Assert.IsType<int>(result);
        }

        [Fact]
        public void SwitchApprovalStatusShouldChangeIsApprovedValueToOpposite()
        {
            //Arrange

            var data = DataMock.Instance;
            var mapper = MapperMock.Instance;
            var editorService = new EditorService(data, mapper);

            data.Editors.Add(new Editor
            {
                Id = 5,
                UserId = "someUserId",
                IsApproved = false
            });

            // Act

            editorService.SwitchApprovalStatus(5);

            var result = data.Editors
                .Find(5)
                .IsApproved;

            // Assert

            Assert.IsType<bool>(result);
            Assert.True(result);
        }

        [Fact]
        public void AllShouldReturnCorrectModel()
        {
            //Arrange

            var data = DataMock.Instance;
            var mapper = MapperMock.Instance;
            var editorService = new EditorService(data, mapper);

            // Act

            var result = editorService.All();

            // Assert

            Assert.IsAssignableFrom<IEnumerable<EditorServiceModel>>(result);
        }
    }
}