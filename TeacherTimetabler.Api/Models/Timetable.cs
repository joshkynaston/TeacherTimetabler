namespace TeacherTimetabler.Api.Models;

public class Timetable : IOwnedEntity
{
    // Implement IOwnedEntity
    public required string UserId { get; set; }
    public required User User { get; set; }

    public int Id { get; set; }
    public required string AcademicYear { get; set; }
    public bool IsBiWeekly { get; set; }

    // Navigation properties
    public ICollection<RecurringItem>? RecurringItems { get; set; }
    public ICollection<Timeslot>? Timeslots { get; set; }
}
