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

        public IActionResult Index()
        {
            return View(_collectionService.GetUserCollections(GetCurrentUserId()));
        }

        [HttpGet]
        public IActionResult CreateCollection()
        {
            var model = new CreateCollectionViewModel { Dictionaries = _dictionaryService.GetAllDictionaries() };
            return View(model);
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

        public IActionResult DeleteCollection(int id)
        {
            _collectionService.DeleteCollection(id);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> ShowCollectionAsync(int id)
        {
            return View(await _collectionService.GetCollectionAsync(id));
        }

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

        public async Task<IActionResult> AddRevisionWordAsync(int id, int collectionId)
        {
            await _collectionService.AddRevisionWordAsync(id, collectionId);
            var dictionaryId = await _dictionaryService.GetDictionaryIdByCollectionIdAsync(collectionId);
            return RedirectToAction("ShowWordsForAdding", new { dictionaryId, collectionId });
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