using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SkyRim.Models;

namespace SkyRim.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<UserDetails> UserDetails { get; set; } = default!;
        public DbSet<Course> Courses { get; set; } = default!;
        public DbSet<UserCourse> UserCourses { get; set; } = default!;
        public DbSet<Lesson> Lessons { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Configure the many-to-many relationship using the UserCourse join entity
            builder.Entity<UserCourse>()
                .HasKey(uc => new { uc.UserId, uc.CourseId }); // Define composite primary key

            builder.Entity<UserCourse>()
                .HasOne(uc => uc.User)               // UserCourse has one IdentityUser
                .WithMany()                         // <--- Use WithMany() without parameter as IdentityUser doesn't have a direct navigation collection
                .HasForeignKey(uc => uc.UserId);    // Foreign key is UserId

            builder.Entity<UserCourse>()
                .HasOne(uc => uc.Course)             // UserCourse has one Course
                .WithMany(c => c.UserCourses)       // Course has many UserCourses
                .HasForeignKey(uc => uc.CourseId);  // Foreign key is CourseId

            builder.Entity<UserDetails>()
                .HasOne(ud => ud.User)         // UserDetails has one IdentityUser
                .WithOne()                     // IdentityUser has one UserDetails
                .HasForeignKey<UserDetails>(ud => ud.Id);

            builder.Entity<Lesson>()
                .HasOne(c => c.Course)
                .WithMany(l => l.Lessons)
                .HasForeignKey(l => l.CourseId)
                .OnDelete(DeleteBehavior.Restrict);

            // Configure the one-to-one relationship for UserDetails
            builder.Entity<UserDetails>()
                .HasOne(ud => ud.User)         // UserDetails has one IdentityUser
                .WithOne()                     // IdentityUser has one UserDetails
                .HasForeignKey<UserDetails>(ud => ud.Id); // Foreign key is UserDetails.Id


        }
    }
}
