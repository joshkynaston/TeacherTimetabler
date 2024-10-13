namespace TeacherTimetabler.Api.Models;

public class ClassEntity : IOwnedEntity
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public string? Subject { get; set; }

    public required string UserEntityId { get; set; }
    public required AppUserEntity UserEntity { get; set; }
}
