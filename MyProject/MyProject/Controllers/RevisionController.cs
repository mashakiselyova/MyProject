using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyProject.Models;
using MyProject.Services;
using MyProject.ViewModels;

namespace MyProject.Controllers
{
    [Authorize]
    public class RevisionController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly CollectionService _collectionService;
        private readonly RevisionService _revisionService;

        public RevisionController(UserManager<User> userManager, CollectionService collectionRepository, 
            RevisionService reviewService)
        {
            _userManager = userManager;
            _collectionService = collectionRepository;
            _revisionService = reviewService;
        }

        public IActionResult Index()
        {
            return View(_collectionService.GetUserCollections(_userManager.GetUserId(HttpContext.User)));
        }

        [HttpGet]
        public IActionResult ReviewCollection(int id)
        {
            var revision = new RevisionViewModel
            {
                CollectionId = id,
                CollectionName = _collectionService.GetCollectionName(id),
                PracticeWords = _revisionService.GetWordsForRevision(id)
            };
            return View(revision);
        }

        [HttpPost]
        public IActionResult ReviewCollection(RevisionViewModel model)
        {
            var resultWords = _revisionService.EvaluateRevision(model.PracticeWords);
            _revisionService.SaveRevisionResult(resultWords);

            var revisionResult = new RevisionResultViewModel
            {
                CollectionId = model.CollectionId,
                CollectionName = model.CollectionName,
                RevisionResultWords = resultWords
            };

            return View("RevisionResult", revisionResult); 
        }
    }
}