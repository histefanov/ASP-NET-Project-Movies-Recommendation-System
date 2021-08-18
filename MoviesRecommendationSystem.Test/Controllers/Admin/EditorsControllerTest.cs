namespace MoviesRecommendationSystem.Test.Controllers.Admin
{
    using System.Collections.Generic;
    using Xunit;
    using MyTested.AspNetCore.Mvc;

    using MoviesRecommendationSystem.Areas.Admin.Controllers;
    using MoviesRecommendationSystem.Services.Editors.Models;

    public class EditorsControllerTests
    {
        [Fact]
        public void AllShouldRequireAdminAuthorizationAndReturnViewWithCorrectModel()
            => MyController<EditorsController>
                .Instance()
                .WithUser(user => user
                    .InRole("Admin"))
                .Calling(c => c.All())
                .ShouldReturn()
                .View(view => view
                    .WithModelOfType<IEnumerable<EditorServiceModel>>());
    }
}
