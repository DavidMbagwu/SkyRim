using System;

namespace SkyRim.Models
{
    public class Course
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Author { get; set; }
        public decimal? Duration { get; set; }
        public DateOnly Date { get; set; } = DateOnly.FromDateTime(DateTime.Now);
        public string? ImageUrl { get; set; }

        public List<Lesson> Lessons { get; set; } = new List<Lesson>();

        public ICollection<UserCourse> UserCourses { get; set; } = new List<UserCourse>();

    }
}
