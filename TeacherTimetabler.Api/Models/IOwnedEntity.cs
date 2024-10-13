namespace TeacherTimetabler.Api.Models;

public interface IOwnedEntity
{
    public string UserEntityId { get; set; }
    public AppUserEntity UserEntity { get; set; }
}
