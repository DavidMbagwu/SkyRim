using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.CodeAnalysis;

namespace SkyRim.Models
{
    public class Lesson
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public string? VideoUrl { get; set; }
        public string? ImgUrl { get; set; }
        public int Order { get; set; }
        public string? Content { get; set; }
        public int CourseId { get; set; }


        [ForeignKey(nameof(CourseId))]
        public Course Course { get; set; } = null!;

    }
}
