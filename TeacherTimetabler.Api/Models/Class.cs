namespace TeacherTimetabler.Api.Models;

public class Class : OwnedEntity
{
  // Properties
  public required string ClassName { get; set; }
  public string? Subject { get; set; }
}
