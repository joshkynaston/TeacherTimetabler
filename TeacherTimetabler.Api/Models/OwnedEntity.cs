namespace TeacherTimetabler.Api.Models;

public abstract class OwnedEntity
{
  public int Id { get; init; }
  public required string TeacherId { get; set; }
  public required Teacher Teacher { get; init; }
}
