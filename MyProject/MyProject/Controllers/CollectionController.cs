using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyProject.DB;
using MyProject.Models;
using MyProject.ViewModels;

namespace MyProject.Controllers
{
    [Authorize]
    public class CollectionController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly CollectionRepository _collectionRepository;
        private readonly VocabularyRepository _vocabularyRepository;

        public CollectionController(UserManager<User> userManager, CollectionRepository collectionRepository, 
            VocabularyRepository vocabularyRepository)
        {
            _userManager = userManager;
            _collectionRepository = collectionRepository;
            _vocabularyRepository = vocabularyRepository;
        }

        public IActionResult Index()
        {
            return View(_collectionRepository.GetUserCollections(GetCurrentUserId()));
        }

        [HttpGet]
        public IActionResult CreateCollection()
        {
            var model = new CreateCollectionViewModel { Vocabularies = _vocabularyRepository.GetAllVocabularies() };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCollectionAsync(CreateCollectionViewModel model)
        {
            var collection = new Collection
            {
                Name = model.CollectionName,
                VocabularyId = model.VocabularyId,
                UserId = GetCurrentUserId()
            };
            await _collectionRepository.CreateCollectionAsync(collection);
            return RedirectToAction("Index");
        }

        public IActionResult DeleteCollection(int id)
        {
            _collectionRepository.DeleteCollection(id);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> ShowCollectionAsync(int id)
        {
            return View(await _collectionRepository.GetCollectionAsync(id));
        }

        public async Task<IActionResult> AddWordAsync(int id, int collectionId)
        {
            await _collectionRepository.AddWordAsync(id, collectionId);
            return RedirectToAction("ShowCollection", new { id = collectionId });
        }

        [HttpGet]
        public IActionResult DeleteRevisionWord(int wordId, int collectionId)
        {
            _collectionRepository.DeleteRevisionWord(wordId);
            return RedirectToAction("ShowCollection", new { id = collectionId });
        }

        private string GetCurrentUserId()
        {
            return _userManager.GetUserId(HttpContext.User);
        }
    }
}