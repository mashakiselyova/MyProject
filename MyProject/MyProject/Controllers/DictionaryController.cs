using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyProject.DB;
using MyProject.Models;
using System.Threading.Tasks;

namespace MyProject.Controllers
{
    public class DictionaryController : Controller
    {
        private readonly DictionaryRepository _dictionaryRepository;
        private readonly CollectionRepository _collectionRepository;

        public DictionaryController(DictionaryRepository dictionaryRepository, CollectionRepository collectionRepository)
        {
            _dictionaryRepository = dictionaryRepository;
            _collectionRepository = collectionRepository;
        }

        public IActionResult Index()
        {
            return View(_dictionaryRepository.GetAllDictionaries());
        }

        [HttpGet]
        public IActionResult AddDictionary()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddDictionaryAsync(string name)
        {
            Dictionary dictionary = new Dictionary { Name = name };
            await _dictionaryRepository.CreateDictionaryAsync(dictionary);
            return RedirectToAction("Index");
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public IActionResult DeleteDictionary(int id)
        {
            _dictionaryRepository.DeleteDictionary(id);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> ShowDictionaryAsync(int id, int collectionId = 0)
        {
            var dictionary = await _dictionaryRepository.GetDictionaryAsync(id);
            if (collectionId != 0)
            {
                var collection = await _collectionRepository.GetCollectionAsync(collectionId);
                dictionary.Collections.Add(collection);
            }
            return View(dictionary);
        }

        [HttpGet]
        public IActionResult CreateWord(int dictionaryId)
        {
            return View(new Word { DictionaryId = dictionaryId });
        }

        [HttpPost]
        public async Task<IActionResult> CreateWordAsync(Word word)
        {
            await _dictionaryRepository.CreateWordAsync(word);
            return RedirectToAction("ShowDictionary", new { id = word.DictionaryId });
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteWordAsync(int wordId)
        {
            var dictionaryId = await _dictionaryRepository.GetDictionaryIdByWordIdAsync(wordId);
            _dictionaryRepository.DeleteWord(wordId);
            return RedirectToAction("ShowDictionary", new { id = dictionaryId });
        }
    }
}