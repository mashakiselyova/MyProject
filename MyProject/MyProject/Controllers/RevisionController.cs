using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyProject.DB;
using MyProject.Models;
using MyProject.ViewModels;

namespace MyProject.Controllers
{
    [Authorize]
    public class RevisionController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly CollectionService _collectionService;
        private readonly RevisionService _reviewService;

        public RevisionController(UserManager<User> userManager, CollectionService collectionRepository, 
            RevisionService reviewService)
        {
            _userManager = userManager;
            _collectionService = collectionRepository;
            _reviewService = reviewService;
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
                PracticeWords = _reviewService.GetWordsForRevision(id)
            };
            return View(revision);
        }

        [HttpPost]
        public IActionResult ReviewCollection(RevisionViewModel model)
        {
            return RedirectToAction("Index"); 
        }
    }
}