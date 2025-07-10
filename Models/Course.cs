using System;

namespace SkyRim.Models
{
    public class Course
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Author { get; set; }
        public decimal? Duration { get; set; }
        public DateOnly Date { get; set; } = DateOnly.FromDateTime(DateTime.Now);
        public string? ImageUrl { get; set; }

        public List<Lesson> Lessons { get; set; } = new List<Lesson>();

        public ICollection<UserCourse> UserCourses { get; set; } = new List<UserCourse>();
        public ICollection<CreatedCourse> CreatedCourses { get; set; } = new List<CreatedCourse>();

    }
}
