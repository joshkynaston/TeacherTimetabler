namespace TeacherTimetabler.Api.Models;

public class Timetable : OwnedEntity
{
  // Properties
  public required string AcademicYear { get; set; }
  public bool IsBiWeekly { get; set; }

  // Navigation properties
  public ICollection<RecurringItem>? RecurringItems { get; set; }
  public ICollection<Timeslot>? Timeslots { get; set; }
}
