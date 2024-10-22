using System.ComponentModel.DataAnnotations;

namespace TeacherTimetabler.Api.Models;

public class Timeslot : OwnedEntity
{
  [MaxLength(50)]
  public required string Name { get; init; } // "Period 1", "Break", etc.
  public TimeSpan StartTime { get; init; }
  public TimeSpan EndTime { get; init; }

  // Foreign keys
  public int TimetableId { get; init; }
  public required Timetable Timetable { get; init; }
}
