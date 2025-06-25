using Microsoft.AspNetCore.Mvc;

namespace SkyRim.Controllers
{
    public class CourseController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
