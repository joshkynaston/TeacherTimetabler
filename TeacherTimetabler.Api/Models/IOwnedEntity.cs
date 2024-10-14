namespace TeacherTimetabler.Api.Models;

public interface IOwnedEntity
{
    public string UserEntityId { get; set; }
    public UserEntity UserEntity { get; set; }
}
