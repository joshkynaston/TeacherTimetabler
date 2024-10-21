using System.ComponentModel.DataAnnotations;

namespace TeacherTimetabler.Api.Models;

public class ItemInstance : IOwnedByTeacher
{
  // Implement IOwnedEntity
  [Key]
  public int EntityId { get; set; }
  public required string TeacherId { get; set; }
  public required Teacher Teacher { get; set; }

  // Properties
  public required string ActivityType { get; set; }
  public string? Title { get; set; }
  public string? Description { get; set; }

  // Foreign keys
  public required int WeekInstanceId { get; set; }
  public int ClassId { get; set; }

  // Navigation properties
  public required WeekInstance WeekInstance { get; set; }
  public Class? Class { get; set; }
}
