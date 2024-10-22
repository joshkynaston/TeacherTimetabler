using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace TeacherTimetabler.Api.Models;

public class Teacher : IdentityUser
{
  // Properties
  [MaxLength(40)]
  public required string FirstName { get; set; }

  [MaxLength(40)]
  public required string LastName { get; set; }
  public bool TimetableIsBiweekly { get; set; }

  // Navigation properties
  public ICollection<Timetable>? Timetables { get; set; }
}
