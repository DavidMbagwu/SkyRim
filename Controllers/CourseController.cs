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
    public class CourseController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _env;
        private readonly UserManager<IdentityUser> _userManager;

        public CourseController(UserManager<IdentityUser> userManager, ApplicationDbContext context, IWebHostEnvironment env)
        {
            _userManager = userManager;
            _context = context;
            _env = env;
        }

        // Only Tutors and Admins can access these actions
        [Authorize(Roles = "Admin,Tutor")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Course course)
        {
            var currentUser = _userManager.GetUserAsync(User).Result;
            if (currentUser == null)
            {
                return RedirectToAction("Login", "Account", new { area = "Identity" });
            }
            if (!ModelState.IsValid)
            {
                return View(course);
                
            }

            var existingCourse = _context.Courses.FirstOrDefault(c => c.Id == course.Id);
            if (existingCourse == null)
            {
                _context.Courses.Add(course);
                _context.CreatedCourses.Add(new CreatedCourse
                {
                    UserId = currentUser.Id,
                    CourseId = course.Id,
                    CreateDate = DateTime.UtcNow
                });

                _context.SaveChanges();
            }
            else
            {
                ModelState.AddModelError("", "Course with this ID already exists.");
                return View(course);
            }
            return RedirectToAction("Manage", "Course");

            }

        [Authorize(Roles = "Admin,Tutor")]
        public IActionResult Edit(string id)
        {
            var course = _context.Courses.FirstOrDefault(c => c.Id == id);
            if (course == null)
            {
                return NotFound();
            }
            return View(course);
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Tutor")]
        public IActionResult Edit(string id, Course course)
        {
            if (id != course.Id)
            {
                return NotFound(); // Or BadRequest()
            }

            ModelState.Remove("Date");
            ModelState.Remove("UserCourses");
            ModelState.Remove("Lessons");

            if (!ModelState.IsValid)
            {
                return View(course);
            }

            var existingCourseInDb = _context.Courses.AsNoTracking().FirstOrDefault(c => c.Id == id);
            if (existingCourseInDb == null)
            {
                return NotFound(); // Course not found in DB (e.g., deleted by another user)
            }

            _context.Courses.Update(course);
            _context.SaveChanges();
            return RedirectToAction("Manage", "Course");

        }

        [Authorize(Roles = "Admin,Tutor")]
        public IActionResult Delete(int id)
        {
            // ...
            return View();
        }

        [Authorize(Roles = "Admin,Tutor")]
        public IActionResult Manage(string searchString)
        {
            var lessonNameList = _context.Lessons.Select(l => new { l.Name, l.Id, CourseId = l.CourseId });
            List<SelectListItem> selectLessonItems = lessonNameList.Select(c => new SelectListItem
            {
                Value = c.Id,
                Text = c.Name
            }).ToList();

            var lessonIdList = _context.Lessons.Select(l => new { l.Id, CourseId = l.CourseId });
            List<SelectListItem> selectLessonIdItems = lessonIdList.Select(c => new SelectListItem
            {
                Value = c.CourseId,
                Text = c.Id
            }).ToList();

            var newLesson = new Lesson();

            ViewBag.Lessons = selectLessonItems;
            ViewBag.LessonsJson = Newtonsoft.Json.JsonConvert.SerializeObject(selectLessonItems);

            ViewBag.LessonIds = selectLessonIdItems;
            ViewBag.LessonIdsJson = Newtonsoft.Json.JsonConvert.SerializeObject(selectLessonIdItems);

            return View();
        }


        // Students (and others) can view courses
        public IActionResult Details(int id)
        {
            // ...
            return View();
        }

        [HttpGet]
        [AllowAnonymous] // Allow anyone to view the list of courses
        public IActionResult GetAll()
        {
            var currentUser = _userManager.GetUserAsync(User).Result;
            if (currentUser == null)
            {
                return RedirectToAction("Login", "Account", new { area = "Identity" });
            }
            var userId = _userManager.GetUserId(User);
            //var existingCourse = _context.CreatedCourses.Where(c => c.UserId == userId);
            var allCourses = _context.Courses.Where(_context => _context.CreatedCourses.Any(c => c.UserId == userId)).ToList();
            return Json(allCourses);

        }


        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ViewAll()
        {
            var allCourses = await _context.Courses.ToListAsync();
            return Json(allCourses);

        }

        public IActionResult AllCourses()
        {
            var lessonNameList = _context.Lessons.Select(l => new { l.Name, l.Id });
            List<SelectListItem> selectLessonItems = lessonNameList.Select(c => new SelectListItem
            {
                Value = c.Id,
                Text = c.Name
            }).ToList();

            var newLesson = new Lesson();

            ViewBag.Lessons = selectLessonItems;
            ViewBag.LessonsJson = Newtonsoft.Json.JsonConvert.SerializeObject(selectLessonItems);

            return View();
        }

        [HttpGet]
        public IActionResult Search(string searchString)
        {
            IQueryable<Course> courses = _context.Courses;
            return View();
        }

        public IActionResult MyCourses()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddCourse(string courseId)
        {
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null)
            {
                return RedirectToAction("Login", "Account", new { area = "Identity" });
            }

            var currentCourse = await _context.Courses.FirstOrDefaultAsync(c => c.Id == courseId);

            _context.UserCourses.Add(new UserCourse
            {
                UserId = currentUser.Id,
                CourseId = courseId,
                EnrollmentDate = DateTime.Now
            });

            _context.SaveChanges();

            return Ok(new { message = "Course added successfully!", courseId = courseId });

        }

        [HttpDelete]
        public async Task<IActionResult> UnaddCourse(string courseId)
        {
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null)
            {
                return RedirectToAction("Login", "Account", new { area = "Identity" });
            }

            var currentCourse = await _context.Courses.FirstOrDefaultAsync(c => c.Id == courseId);
            if (currentCourse != null)
            {
                var currentUserCourse = await _context.UserCourses.FindAsync(currentUser.Id, currentCourse.Id);
                _context.UserCourses.Remove(currentUserCourse);
                _context.SaveChanges();

                return Ok(new { message = "Course unadded successfully!", courseId = courseId });
            }
            else
            {
                return NotFound();
            }

        }

        [HttpGet]
        public IActionResult CoursePage(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return BadRequest("Course Id cannot be empty.");
            }

            var course = _context.Courses.FirstOrDefault(c => c.Id == id);

            if (course == null)
            {
                // If no lesson is found with that ID, return a 404 Not Found
                return NotFound($"Course with Id {id} not found.");
            }

            return View(course);
        }
    }
}
