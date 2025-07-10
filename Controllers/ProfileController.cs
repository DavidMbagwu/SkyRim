using Microsoft.AspNetCore.Mvc;
using SkyRim.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using SkyRim.Data;
using Microsoft.AspNetCore.Identity;

namespace SkyRim.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public ProfileController(UserManager<IdentityUser> userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> UserDetailsForm()
        {
            var currentUser = await _userManager.GetUserAsync(User);

            if (currentUser == null)
            {
                return RedirectToAction("Login", "Account", new { area = "Identity" });
            }

            var userDetails = await _context.UserDetails.FirstOrDefaultAsync(ud => ud.Id == currentUser.Id);

            if (userDetails == null)
            {
                userDetails = new UserDetails { Id = currentUser.Id };
            }

            ViewBag.Genders = Choices.ToSelectListItems(Choices.Genders, userDetails.Gender);
            ViewBag.Nationalities = Choices.ToSelectListItems(Choices.Nationality, userDetails.Nationality);
            ViewBag.Races = Choices.ToSelectListItems(Choices.races, userDetails.Race);
            return View(userDetails);


        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> UserDetailsForm(UserDetails userDetails)
        {
            if (!ModelState.IsValid)
            {
                // If validation fails, return to the form to show errors
                return View(userDetails);
            }

            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null || currentUser.Id != userDetails.Id)
            {
                // Security check: Ensure the user submitting the form is the one logged in
                return Forbid(); // Or RedirectToAction("AccessDenied", "Account", new { area = "Identity" });
            }

            var existingDetails = await _context.UserDetails.FirstOrDefaultAsync(ud => ud.Id == userDetails.Id);

            if (existingDetails == null)
            {
                // Add new UserDetails if they don't exist
                _context.UserDetails.Add(userDetails);
            }
            else
            {
                // Update existing UserDetails
                _context.Entry(existingDetails).CurrentValues.SetValues(userDetails);
            }

            await _context.SaveChangesAsync();

            // Redirect to the dashboard or home page after saving details
            return RedirectToAction("Dashboard", "Home"); // Assuming a Dashboard action in HomeController
        }

    }
}
