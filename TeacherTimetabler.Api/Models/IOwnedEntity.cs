namespace TeacherTimetabler.Api.Models;

public interface IOwnedEntity
{
    public string TeacherId { get; set; }
    public Teacher Teacher { get; set; }
}
