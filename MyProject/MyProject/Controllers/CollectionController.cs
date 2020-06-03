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
        private readonly CollectionService _collectionService;
        private readonly DictionaryService _dictionaryService;

        public CollectionController(UserManager<User> userManager, CollectionService collectionService, 
            DictionaryService dictionaryService)
        {
            _userManager = userManager;
            _collectionService = collectionService;
            _dictionaryService = dictionaryService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View(_collectionService.GetUserCollections(GetCurrentUserId()));
        }

        [HttpGet]
        public IActionResult CreateCollection()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateCollectionAsync(CreateCollectionViewModel model)
        {
            var collection = new Collection
            {
                Name = model.CollectionName,
                DictionaryId = model.DictionaryId,
                UserId = GetCurrentUserId()
            };
            await _collectionService.CreateCollectionAsync(collection);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult DeleteCollection(int id)
        {
            _collectionService.DeleteCollection(id);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> ShowCollectionAsync(int id)
        {
            return View(await _collectionService.GetCollectionAsync(id));
        }

        [HttpGet]
        public async Task<IActionResult> ShowWordsForAddingAsync(int dictionaryId, int collectionId)
        {
            var dictionary = await _dictionaryService.GetDictionaryAsync(dictionaryId);
            var collection = await _collectionService.GetCollectionAsync(collectionId);
            foreach(var revisionWord in collection.RevisionWords)
            {
                dictionary.Words.Remove(dictionary.Words.Find(w => w.Id == revisionWord.Word.Id));
            }
            var model = new AddRevisionWordViewModel
            {
                DictionaryId = dictionaryId,
                CollectionId = collectionId,
                DictionaryName = dictionary.Name,
                Words = dictionary.Words
            };
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> AddRevisionWordAsync(int id, int collectionId)
        {
            await _collectionService.AddRevisionWordAsync(id, collectionId);
            var collection = await _collectionService.GetCollectionAsync(collectionId);
            return RedirectToAction("ShowWordsForAdding", new { dictionaryId = collection.Id, collectionId });
        }

        [HttpGet]
        public IActionResult DeleteRevisionWord(int wordId, int collectionId)
        {
            _collectionService.DeleteRevisionWord(wordId);
            return RedirectToAction("ShowCollection", new { id = collectionId });
        }

        private string GetCurrentUserId()
        {
            return _userManager.GetUserId(HttpContext.User);
        }
    }
}