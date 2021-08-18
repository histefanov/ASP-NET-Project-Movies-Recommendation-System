namespace MoviesRecommendationSystem.Test.Routing.Admin
{
    using MyTested.AspNetCore.Mvc;
    using Xunit;
    
    using MoviesRecommendationSystem.Areas.Admin.Controllers;

    public class EditorsControllerTest
    {
        [Fact]
        public void AllShouldBeMappedAccurately()
            => MyRouting
                .Configuration()
                .ShouldMap("/Admin/Editors/All")
                .To<EditorsController>(c => c.All());

        [Fact]
        public void SwitchApprovalStatusShouldBeMappedAccurately()
            => MyRouting
                .Configuration()
                .ShouldMap("/Admin/Editors/SwitchApprovalStatus/1")
                .To<EditorsController>(c => c.SwitchApprovalStatus(1));
    }
}
