using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyProject.Models;
using System.Threading.Tasks;

namespace MyProject.Components
{
    public class Login : ViewComponent
    {
        private readonly UserManager<User> _userManager;

        public Login(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            User user = await _userManager.GetUserAsync(HttpContext.User);
            return View(user);
        }
    }
}
