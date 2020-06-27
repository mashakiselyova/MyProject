using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyProject.Models;
using MyProject.Services;
using MyProject.ViewModels;

namespace MyProject.Controllers
{
    [Authorize]
    public class CollectionController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly CollectionService _collectionService;

        public CollectionController(UserManager<User> userManager, CollectionService collectionService)
        {
            _userManager = userManager;
            _collectionService = collectionService;
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
        public async Task<IActionResult> ShowWordsForAddingAsync([FromServices] DictionaryService dictionaryService,
            int dictionaryId, int collectionId)
        {
            var dictionary = await dictionaryService.GetDictionaryAsync(dictionaryId);
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
            var dictionaryId = await _collectionService.GetDictionaryIdByCollectionIdAsync(collectionId);
            return RedirectToAction("ShowWordsForAdding", new { dictionaryId, collectionId });
        }

        [HttpGet]
        public IActionResult DeleteRevisionWord(int wordId, int collectionId)
        {
            _collectionService.DeleteRevisionWord(wordId);
            return RedirectToAction("ShowCollection", new { id = collectionId });
        }

        public async Task<IActionResult> CreateWordForRevisionAsync(int collectionId)
        {
            return View(new CreateWordForRevisionViewModel
            {
                CollectionId = collectionId,
                DictionaryId = await _collectionService.GetDictionaryIdByCollectionIdAsync(collectionId)
            });
        }

        [HttpPost]
        public async Task<IActionResult> CreateWordForRevisionAsync([FromServices] WordService wordService, 
            CreateWordForRevisionViewModel model)
        {
            var word = new Word
            {
                DictionaryId = model.DictionaryId,
                Original = model.Original,
                Translation = model.Translation
            };
            await wordService.CreateWordAsync(word);
            int wordId = await wordService.GetWordIdAsync(word.Original, word.Translation);
            await _collectionService.AddRevisionWordAsync(wordId, model.CollectionId);
            return RedirectToAction("ShowCollection", new { id = model.CollectionId });
        }

        public async Task<IActionResult> ResetProgressAsync(int wordId, int collectionId)
        {
            await _collectionService.ResetProgressAsync(wordId);
            return RedirectToAction("ShowCollection", new { id = collectionId });
        }

        private string GetCurrentUserId()
        {
            return _userManager.GetUserId(HttpContext.User);
        }
    }
}