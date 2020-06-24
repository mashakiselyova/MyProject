using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyProject.Models;
using MyProject.Services;
using System.Threading.Tasks;

namespace MyProject.Controllers
{
    public class DictionaryController : Controller
    {
        private readonly DictionaryService _dictionaryService;
        private readonly WordService _wordService;

        public DictionaryController(DictionaryService dictionaryService, WordService wordService)
        {
            _dictionaryService = dictionaryService;
            _wordService = wordService;
        }

        public IActionResult Index()
        {
            return View(_dictionaryService.GetAllDictionaries());
        }

        [HttpGet]
        public IActionResult AddDictionary()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddDictionaryAsync(Dictionary dictionary)
        {
            await _dictionaryService.CreateDictionaryAsync(dictionary);
            return RedirectToAction("Index");
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public IActionResult DeleteDictionary(int id)
        {
            _dictionaryService.DeleteDictionary(id);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> ShowDictionaryAsync(int id)
        {
            var dictionary = await _dictionaryService.GetDictionaryAsync(id);
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
            await _wordService.CreateWordAsync(word);
            return RedirectToAction("ShowDictionary", new { id = word.DictionaryId });
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> EditWordAsync(int wordId)
        {
            var word = await _wordService.GetWordAsync(wordId);
            return View(word);
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public IActionResult EditWord(Word word)
        {
            _wordService.EditWord(word);
            return RedirectToAction("ShowDictionary", new { id = word.DictionaryId });
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteWordAsync(int wordId)
        {
            var dictionaryId = await _dictionaryService.GetDictionaryIdByWordIdAsync(wordId);
            _wordService.DeleteWord(wordId);
            return RedirectToAction("ShowDictionary", new { id = dictionaryId });
        }
    }
}