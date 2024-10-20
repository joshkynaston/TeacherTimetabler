namespace TeacherTimetabler.Api.Models;

public class Timeslot : IOwnedEntity
{
    // Implement IOwnedEntity
    public required string UserId { get; set; }
    public required User User { get; set; }

    public int Id { get; set; } // PK
    public required string Name { get; set; } // "Period 1", "Break", etc.
    public TimeSpan StartTime { get; set; }
    public TimeSpan EndTime { get; set; }

    // Foreign keys
    public int TimetableId { get; set; }
    public required Timetable Timetable { get; set; }
}
