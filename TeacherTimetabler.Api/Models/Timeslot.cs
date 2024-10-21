using System.ComponentModel.DataAnnotations;

namespace TeacherTimetabler.Api.Models;

public class Timeslot : IOwnedByTeacher
{
  // Implement IOwnedEntity
  [Key]
  public int EntityId { get; set; }
  public required string TeacherId { get; set; }
  public required Teacher Teacher { get; set; }

  public required string Name { get; set; } // "Period 1", "Break", etc.
  public TimeSpan StartTime { get; set; }
  public TimeSpan EndTime { get; set; }

  // Foreign keys
  public int TimetableId { get; set; }
  public required Timetable Timetable { get; set; }
}
