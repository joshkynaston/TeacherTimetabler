namespace TeacherTimetabler.Api.Models;

public interface IOwnedEntity
{
    public string UserId { get; set; }
    public User User { get; set; }
}
