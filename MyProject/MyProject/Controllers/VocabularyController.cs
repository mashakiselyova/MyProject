using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using MyProject.Models;
using MyProject.DB;

namespace MyProject.Controllers
{
    [Authorize]
    public class VocabularyController : Controller
    {

        private UserManager<User> _userManager;
        private VocabularyService _vocabularyService;

        public VocabularyController(UserManager<User> userManager, VocabularyService vocabularyService)
        {
            _userManager = userManager;
            _vocabularyService = vocabularyService;
        }

        public async Task<IActionResult> IndexAsync()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            user.Vocabularies = _vocabularyService.GetVocabulariesByUserId(user.Id);
            return View(user.Vocabularies);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(string name)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);

            Vocabulary vocabulary = new Vocabulary { Name = name, UserId = user.Id };
            await _vocabularyService.AddVocabularyAsync(vocabulary);
            return RedirectToAction("Index");
        }
    }
}