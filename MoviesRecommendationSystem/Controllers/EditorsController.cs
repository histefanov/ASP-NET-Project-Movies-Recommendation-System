namespace MoviesRecommendationSystem.Controllers
{
    using System.Linq;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using MoviesRecommendationSystem.Data;
    using MoviesRecommendationSystem.Data.Models;
    using MoviesRecommendationSystem.Infrastructure;
    using MoviesRecommendationSystem.Models.Editors;

    public class EditorsController : Controller
    {
        private readonly MoviesRecommendationDbContext data;

        public EditorsController(MoviesRecommendationDbContext data) 
            => this.data = data;

        [Authorize]
        public IActionResult Create()
            => View();

        [HttpPost]
        [Authorize]
        public IActionResult Create(BecomeEditorFormModel editor)
        {
            var userId = this.User.GetId();

            var userIsAlreadyEditor = this.data
                .Editors
                .Any(d => d.UserId == userId);

            if (userIsAlreadyEditor)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return View(editor);
            }

            var editorData = new Editor
            {
                FirstName = editor.FirstName,
                LastName = editor.LastName,
                BirthDate = editor.BirthDate,
                UserId = userId
            };

            this.data.Editors.Add(editorData);

            this.data.SaveChanges();

            return RedirectToAction(nameof(HomeController.Index), "Home");
        }
    }
}
