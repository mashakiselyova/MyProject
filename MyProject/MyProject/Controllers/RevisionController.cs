using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyProject.DB;
using MyProject.Models;

namespace MyProject.Controllers
{
    [Authorize]
    public class RevisionController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly CollectionService _collectionService;
        private readonly ReviewService _reviewService;

        public RevisionController(UserManager<User> userManager, CollectionService collectionRepository, 
            ReviewService reviewService)
        {
            _userManager = userManager;
            _collectionService = collectionRepository;
            _reviewService = reviewService;
        }

        public IActionResult Index()
        {
            return View(_collectionService.GetUserCollections(_userManager.GetUserId(HttpContext.User)));
        }

        public IActionResult ReviewCollection(int id)
        {
            return View(_reviewService.GetWordsForRevision(id));
        }
    }
}