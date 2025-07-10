using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.CodeAnalysis;

namespace SkyRim.Models
{
    public class Lesson
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Name { get; set; }
        public string? Description { get; set; }
        public string? VideoUrl { get; set; }
        public string? ImgUrl { get; set; }
        public int Order { get; set; }
        public string? Content { get; set; }
        public string CourseId { get; set; }


        [ForeignKey(nameof(CourseId))]
        public Course Course { get; set; } = null!;

    }
}
