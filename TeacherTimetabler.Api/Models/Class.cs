using System.ComponentModel.DataAnnotations;

namespace TeacherTimetabler.Api.Models;

public class Class : IOwnedByTeacher
{
  [Key]
  public int EntityId { get; set; }
  public required string TeacherId { get; set; }
  public required Teacher Teacher { get; set; }

  public required string ClassName { get; set; }
  public string? Subject { get; set; }
}
