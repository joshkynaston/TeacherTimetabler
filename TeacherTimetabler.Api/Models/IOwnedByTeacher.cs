using System.ComponentModel.DataAnnotations;

namespace TeacherTimetabler.Api.Models;

public interface IOwnedByTeacher
{
  [Key]
  public int EntityId { get; set; }
  public string TeacherId { get; set; }
  public Teacher Teacher { get; set; }
}
