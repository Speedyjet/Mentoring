using Mentoring.BL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Mentoring.Controllers
{
    public class AdminController : Controller
    {
        private IUserService _userService;
        public AdminController(IUserService userService)
        {
            _userService = userService;
        }
        [Authorize(Roles = "Administrator")]
        [Route("/Users")]
        public IActionResult Index()
        {
            var usersList = _userService.GetUsers();
            return View(usersList);
        }
    }
}
