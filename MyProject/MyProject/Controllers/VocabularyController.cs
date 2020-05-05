using Microsoft.AspNetCore.Mvc;
using MyProject.DB;
using MyProject.Models;
using System.Threading.Tasks;

namespace MyProject.Controllers
{
    public class VocabularyController : Controller
    {
        private VocabularyService _vocabularyService;

        public VocabularyController(VocabularyService vocabularyService)
        {
            _vocabularyService = vocabularyService;
        }

        public IActionResult Index()
        {
            return View(_vocabularyService.GetAllVocabularies());
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
            await _vocabularyService.AddVocabularyAsync(vocabulary);
            return RedirectToAction("Index");
        }

        public IActionResult ShowVocabulary(int id)
        {
            return View(_vocabularyService.GetVocabulary(id));
        }

    }
}