﻿namespace MoviesRecommendationSystem.Controllers
{
    using MoviesRecommendationSystem.Infrastructure;
    using MoviesRecommendationSystem.Models.Editors;
    using MoviesRecommendationSystem.Services.Editors;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    public class EditorsController : Controller
    {
        private readonly IEditorsService editorsService;

        public EditorsController(IEditorsService editorsService)
            => this.editorsService = editorsService;

        [Authorize]
        public IActionResult Become()
            => View();

        [HttpPost]
        [Authorize]
        public IActionResult Become(BecomeEditorFormModel editor)
        {
            var userId = this.User.GetId();

            var userIsAlreadyEditor = this.editorsService.UserIsEditor(userId);

            if (userIsAlreadyEditor)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return View(editor);
            }

            this.editorsService.Create(
                editor.FirstName,
                editor.LastName,
                editor.BirthDate,
                userId);

            return RedirectToAction(nameof(HomeController.Index), "Home");
        }
    }
}
