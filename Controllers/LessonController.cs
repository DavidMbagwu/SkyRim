using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol;
using SkyRim.Data;
using SkyRim.Models;

namespace SkyRim.Controllers
{
    [Authorize]
    public class LessonController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _env;
        private readonly UserManager<IdentityUser> _userManager;

        public LessonController(UserManager<IdentityUser> userManager, ApplicationDbContext context, IWebHostEnvironment env)
        {
            _userManager = userManager;
            _context = context;
            _env = env;
        }

        // Only Tutors and Admins can access these actions
        [Authorize(Roles = "Admin,Tutor")]
        public async Task<IActionResult> Create()
        {
            var courseNameList = await _context.Courses.Select(c => new { c.Id, c.Name }).ToListAsync();

            List<SelectListItem> selectCourseItems = courseNameList.Select(c => new SelectListItem
            {
                Value = c.Id,
                Text = c.Name,
            }).ToList();


            ViewBag.Courses = selectCourseItems;
            return View();
        }


        [HttpPost]
        public IActionResult Create(Lesson lesson)
        {
            var currentUser = _userManager.GetUserAsync(User).Result;
            if (currentUser == null)
            {
                return RedirectToAction("Login", "Account", new { area = "Identity" });
            }
            ModelState.Remove("Course");
            if (!ModelState.IsValid)
            {
                return View(lesson);
            }

            var existingLesson = _context.Lessons.FirstOrDefault(c => c.Id == lesson.Id);
            if (existingLesson == null)
            {
                _context.Lessons.Add(lesson);
                _context.SaveChanges();
            }
            else
            {
                ModelState.AddModelError("", "Lesson with this ID already exists.");
                return View(lesson);
            }
            return RedirectToAction("Manage", "Course");
        }

        [Authorize(Roles = "Admin,Tutor")]
        public IActionResult Edit(string id)
        {
            var lesson = _context.Lessons.FirstOrDefault(c => c.Id == id);
            if (lesson == null)
            {
                return NotFound();
            }
            return View(lesson);
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Tutor")]
        public IActionResult Edit(string id, Lesson lesson)
        {
            if (id != lesson.Id)
            {
                return NotFound(); // Or BadRequest()
            }

            ModelState.Remove("CourseId");

            if (!ModelState.IsValid)
            {
                return View(lesson);
            }

            var existingLessonInDb = _context.Lessons.AsNoTracking().FirstOrDefault(c => c.Id == id);
            if (existingLessonInDb == null)
            {
                return NotFound(); // Course not found in DB (e.g., deleted by another user)
            }

            _context.Lessons.Update(lesson);
            _context.SaveChanges();
            return RedirectToAction("Manage", "Lesson");

        }

        [Authorize(Roles = "Admin,Tutor")]
        public IActionResult Delete(int id)
        {
            // ...
            return View();
        }

        [Authorize(Roles = "Admin,Tutor")]
        public IActionResult Manage()
        {
            // ...
            return View();
        }

        // Students (and others) can view courses
        public IActionResult Details(int id)
        {
            // ...
            return View();
        }

        [HttpGet]
        public IActionResult LessonPage(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return BadRequest("Lesson ID cannot be empty.");
            }

            var lesson = _context.Lessons.FirstOrDefault(l => l.Id == id);

            if (lesson == null)
            {
                // If no lesson is found with that ID, return a 404 Not Found
                return NotFound($"Lesson with ID {id} not found.");
            }

            return View(lesson);
        }

    }
}
