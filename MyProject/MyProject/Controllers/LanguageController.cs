using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyProject.DB;
using MyProject.Models;
using System.Threading.Tasks;

namespace MyProject.Controllers
{
    public class LanguageController : Controller
    {
        private readonly LanguageRepository _languageRepository;
        private readonly CollectionRepository _collectionRepository;

        public LanguageController(LanguageRepository languageRepository, CollectionRepository collectionRepository)
        {
            _languageRepository = languageRepository;
            _collectionRepository = collectionRepository;
        }

        public IActionResult Index()
        {
            return View(_languageRepository.GetAllLanguages());
        }

        [HttpGet]
        public IActionResult AddLanguage()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddLanguageAsync(string name)
        {
            Language language = new Language { Name = name };
            await _languageRepository.CreateLanguageAsync(language);
            return RedirectToAction("Index");
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public IActionResult DeleteLanguage(int id)
        {
            _languageRepository.DeleteLanguage(id);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> ShowLanguageAsync(int id, int collectionId = 0)
        {
            var language = await _languageRepository.GetLanguageAsync(id);
            if (collectionId != 0)
            {
                var collection = await _collectionRepository.GetCollectionAsync(collectionId);
                language.Collections.Add(collection);
            }
            return View(language);
        }

        [HttpGet]
        public IActionResult CreateWord(int languageId)
        {
            return View(new Word { LanguageId = languageId });
        }

        [HttpPost]
        public async Task<IActionResult> CreateWordAsync(Word word)
        {
            await _languageRepository.CreateWordAsync(word);
            return RedirectToAction("ShowLanguage", new { id = word.LanguageId });
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteWordAsync(int wordId)
        {
            var languageId = await _languageRepository.GetLanguageIdByWordIdAsync(wordId);
            _languageRepository.DeleteWord(wordId);
            return RedirectToAction("ShowLanguage", new { id = languageId });
        }
    }
}