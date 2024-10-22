namespace TeacherTimetabler.Api.Models;

public class RecurringItem : OwnedEntity
{
  // Properties
  public string? WeekType { get; set; } // Week A/B 1/2 for biweekly timetables
  public required DayOfWeek DayOfWeek { get; set; }
  public required string ActivityType { get; set; } // e.g. Lesson, Planning, PPA, Lunch

  // Foreign keys
  public int TimetableId { get; set; }
  public int TimeslotId { get; set; }
  public int ClassId { get; set; }

  // Navigation properties
  public required Timetable Timetable { get; set; }
  public required Timeslot Timeslot { get; set; }
  public Class? Class { get; set; }
}
