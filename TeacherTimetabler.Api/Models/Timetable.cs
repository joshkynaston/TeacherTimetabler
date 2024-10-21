using System.ComponentModel.DataAnnotations;

namespace TeacherTimetabler.Api.Models;

public class Timetable : IOwnedByTeacher
{
  // Implement IOwnedEntity
  [Key]
  public int EntityId { get; set; }
  public required string TeacherId { get; set; }
  public required Teacher Teacher { get; set; }

  public required string AcademicYear { get; set; }
  public bool IsBiWeekly { get; set; }

  // Navigation properties
  public ICollection<RecurringItem>? RecurringItems { get; set; }
  public ICollection<Timeslot>? Timeslots { get; set; }
}
