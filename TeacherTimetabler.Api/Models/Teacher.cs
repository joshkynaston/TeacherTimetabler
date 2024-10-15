using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace TeacherTimetabler.Api.Models;

public class Teacher : IdentityUser
{
    [Required]
    public required string FirstName { get; set; }

    [Required]
    public required string LastName { get; set; }

    public string? DefaultTimetableType { get; set; }

    public ICollection<Timetable>? Timetables { get; set; }
}
