using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SkyRim.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        public IActionResult ManageUsers()
        {
            // ... logic to list and manage users ...
            return View();
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
