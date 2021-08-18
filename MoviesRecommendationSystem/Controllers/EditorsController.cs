namespace MoviesRecommendationSystem.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using MoviesRecommendationSystem.Infrastructure;
    using MoviesRecommendationSystem.Models.Editors;
    using MoviesRecommendationSystem.Services.Editors;

    using static WebConstants;
    using static Common.ControllerConstants.Editors;

    public class EditorsController : Controller
    {
        private readonly IEditorService editorService;

        public EditorsController(IEditorService editorService)
            => this.editorService = editorService;

        [Authorize]
        public IActionResult Become()
            => View();

        [HttpPost]
        [Authorize]
        public IActionResult Become(BecomeEditorFormModel editor)
        {
            var userId = User.GetId();

            var userIsAlreadyEditor = this.editorService.UserIsEditor(userId);

            if (userIsAlreadyEditor)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return View(editor);
            }

            this.editorService.Create(
                editor.FirstName,
                editor.LastName,
                editor.BirthDate,
                userId);

            TempData[GlobalMessageKey] = SubmissionSentMessage;

            return RedirectToAction(nameof(HomeController.Index), HomeControllerName);
        }
    }
}
