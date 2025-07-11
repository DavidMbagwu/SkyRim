﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace SkyRim.Models
{
    public class UserDetails
    {
        [Key]
        [Required]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [Required(ErrorMessage = "First Name is required.")]
        [StringLength(100, ErrorMessage = "First Name cannot exceed 100 characters.")]
        public string FirstName { get; set; } = string.Empty;

        [StringLength(100, ErrorMessage = "Middle Name cannot exceed 100 characters.")]
        public string? MiddleName { get; set; }

        [Required(ErrorMessage = "Last Name is required.")]
        [StringLength(100, ErrorMessage = "Last Name cannot exceed 100 characters.")]
        public string LastName { get; set; } = string.Empty;


        [Display(Name = "Gender")]
        [Required(ErrorMessage = "Gender is required.")]
        public string Gender { get; set; } = string.Empty;

        [Display(Name = "Nationality")]
        [Required(ErrorMessage = "Nationality is required.")]
        public string Nationality { get; set; } = string.Empty;

        [Display(Name = "Race")]
        [Required(ErrorMessage = "Race is required.")]
        public string Race { get; set; } = string.Empty;


        [StringLength(200, ErrorMessage = "LinkedIn URL cannot exceed 200 characters.")]
        [Url(ErrorMessage = "Please enter a valid URL for LinkedIn.")] // Validation for URL format
        public string? LinkedInUrl { get; set; }

        [StringLength(200, ErrorMessage = "GitHub URL cannot exceed 200 characters.")]
        [Url(ErrorMessage = "Please enter a valid URL for GitHub.")] // Validation for URL format
        public string? GitHubUrl { get; set; }

        // Navigation property for the one-to-one relationship back to the IdentityUser
        // This explicitly tells EF Core that 'Id' is also a foreign key to IdentityUser.Id
        [ForeignKey("Id")]
        public IdentityUser? User { get; set; } = null;
    }
}
