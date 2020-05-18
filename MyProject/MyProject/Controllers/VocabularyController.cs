using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyProject.DB;
using MyProject.Models;
using System.Threading.Tasks;

namespace MyProject.Controllers
{
    public class VocabularyController : Controller
    {
        private readonly VocabularyRepository _vocabularyRepository;
        private readonly CollectionRepository _collectionRepository;

        public VocabularyController(VocabularyRepository vocabularyRepository, CollectionRepository collectionRepository)
        {
            _vocabularyRepository = vocabularyRepository;
            _collectionRepository = collectionRepository;
        }

        public IActionResult Index()
        {
            return View(_vocabularyRepository.GetAllVocabularies());
        }

        [HttpGet]
        public IActionResult CreateVocabulary()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateVocabularyAsync(string name)
        {
            Vocabulary vocabulary = new Vocabulary { Name = name };
            await _vocabularyRepository.CreateVocabularyAsync(vocabulary);
            return RedirectToAction("Index");
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public IActionResult DeleteVocabulary(int id)
        {
            _vocabularyRepository.DeleteVocabulary(id);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> ShowVocabularyAsync(int id, int collectionId = 0)
        {
            var vocabulary = await _vocabularyRepository.GetVocabularyAsync(id);
            if (collectionId != 0)
            {
                var collection = await _collectionRepository.GetCollectionAsync(collectionId);
                vocabulary.Collections.Add(collection);
            }
            return View(vocabulary);
        }

        [HttpGet]
        public IActionResult CreateWord(int vocabularyId)
        {
            return View(new Word { VocabularyId = vocabularyId });
        }

        [HttpPost]
        public async Task<IActionResult> CreateWordAsync(Word word)
        {
            await _vocabularyRepository.CreateWordAsync(word);
            return RedirectToAction("ShowVocabulary", new { id = word.VocabularyId });
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public IActionResult DeleteWord(int wordId, int vocabularyId)
        {
            _vocabularyRepository.DeleteWord(wordId);
            return RedirectToAction("ShowVocabulary", new { id = vocabularyId });
        }
    }
}