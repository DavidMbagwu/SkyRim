using System;
using Microsoft.AspNetCore.Identity;
using SkyRim.Models;
using SkyRim.Data;

namespace SkyRim.Models
{
    public class UserCourse
    {
        public string UserId { get; set; } // Foreign Key to ApplicationUser (IdentityUser.Id is string)
        public int CourseId { get; set; } // Foreign Key to Course

        public DateTime EnrollmentDate { get; set; } = DateTime.UtcNow; // Additional property for the relationship

        // Navigation properties to the related entities
        public IdentityUser User { get; set; } = null!; // Required navigation property
        public Course Course { get; set; } = null!;
    }
}
