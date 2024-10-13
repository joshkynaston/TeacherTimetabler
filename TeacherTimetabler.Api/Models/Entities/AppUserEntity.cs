using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace TeacherTimetabler.Api.Models.Entities;

public class AppUserEntity : IdentityUser { 
    [Required]
    public required string FirstName { get; set; }

    [Required]
    public required string LastName { get; set; }
}
