namespace TeacherTimetabler.Api.Models;

public class Class : IOwnedEntity
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public string? Subject { get; set; }

    public required string UserId { get; set; }
    public required User User { get; set; }
}
