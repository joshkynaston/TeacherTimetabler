namespace TeacherTimetabler.Api.Models;

public class Class : IOwnedEntity
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public string? Subject { get; set; }

    public required string TeacherId { get; set; }
    public required Teacher Teacher { get; set; }
}
