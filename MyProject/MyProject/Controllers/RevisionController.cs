using Microsoft.AspNetCore.Mvc;

namespace MyProject.Controllers
{
    public class RevisionController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}