namespace TeacherTimetabler.Api.Models;

public interface IOwnedByTeacher
{
  public int Id { get; set; }
  public string TeacherId { get; set; }
  public Teacher Teacher { get; set; }
}
