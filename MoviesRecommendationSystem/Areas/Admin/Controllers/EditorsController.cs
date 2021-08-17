namespace MoviesRecommendationSystem.Areas.Admin.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using MoviesRecommendationSystem.Services.Editors;

    public class EditorsController : AdminController
    {
        private readonly IEditorService editorService;

        public EditorsController(IEditorService editorService) 
            => this.editorService = editorService;

        public IActionResult All() => View(this.editorService.All());

        public IActionResult SwitchApprovalStatus(int id)
        {
            this.editorService.SwitchApprovalStatus(id);

            return RedirectToAction(nameof(All));
        }
    }
}
