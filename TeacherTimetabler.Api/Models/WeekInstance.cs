using System.ComponentModel.DataAnnotations;

namespace TeacherTimetabler.Api.Models;

public class WeekInstance : IOwnedByTeacher
{
  // Implement IOwnedEntity
  [Key]
  public int EntityId { get; set; }
  public required string TeacherId { get; set; }
  public required Teacher Teacher { get; set; }

  // Properties
  public int WeekNumber { get; set; }
  public DateTime StartDate { get; set; }
  public string? WeekType { get; set; }
  public bool IsHoliday { get; set; }

  // Foreign keys
  public required int TimetableId { get; set; }

  // Navigation properties
  public required Timetable Timetable { get; set; }
  public ICollection<ItemInstance>? ItemInstances { get; set; }
}
