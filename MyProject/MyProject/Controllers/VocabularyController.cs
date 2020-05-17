using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyProject.DB;
using MyProject.Models;
using System.Threading.Tasks;

namespace MyProject.Controllers
{
    public class VocabularyController : Controller
    {
        private VocabularyRepository _vocabularyRepository;

        public VocabularyController(VocabularyRepository vocabularyRepository)
        {
            _vocabularyRepository = vocabularyRepository;
        }

        public IActionResult Index()
        {
            return View(_vocabularyRepository.GetAllVocabularies());
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(string name)
        {
            Vocabulary vocabulary = new Vocabulary { Name = name };
            await _vocabularyRepository.CreateVocabularyAsync(vocabulary);
            return RedirectToAction("Index");
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public IActionResult Delete(int id)
        {
            _vocabularyRepository.DeleteVocabulary(id);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> ShowVocabularyAsync(int id)
        {
            var result = await _vocabularyRepository.GetVocabularyAsync(id);
            return View(result);
        }

        [HttpGet]
        public IActionResult CreateWord(int vocabularyId)
        {
            return View(new Word { VocabularyId = vocabularyId });
        }

        [HttpPost]
        public async Task<IActionResult> CreateWord(Word word)
        {
            await _vocabularyRepository.CreateWord(word);
            return RedirectToAction("ShowVocabulary", new { id = word.VocabularyId });
        }


    }
}