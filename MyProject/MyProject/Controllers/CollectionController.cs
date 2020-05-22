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
        private readonly DictionaryRepository _dictionaryRepository;

        public CollectionController(UserManager<User> userManager, CollectionRepository collectionRepository, 
            DictionaryRepository dictionaryRepository)
        {
            _userManager = userManager;
            _collectionRepository = collectionRepository;
            _dictionaryRepository = dictionaryRepository;
        }

        public IActionResult Index()
        {
            return View(_collectionRepository.GetUserCollections(GetCurrentUserId()));
        }

        [HttpGet]
        public IActionResult CreateCollection()
        {
            var model = new CreateCollectionViewModel { Dictionaries = _dictionaryRepository.GetAllDictionaries() };
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

        public async Task<IActionResult> ShowWordsForAddingAsync(int dictionaryId, int collectionId)
        {
            var dictionary = await _dictionaryRepository.GetDictionaryAsync(dictionaryId);
            var collection = await _collectionRepository.GetCollectionAsync(collectionId);
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
            await _collectionRepository.AddRevisionWordAsync(id, collectionId);
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