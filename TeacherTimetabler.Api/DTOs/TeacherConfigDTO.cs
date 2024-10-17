using System.ComponentModel.DataAnnotations;

namespace TeacherTimetabler.Api.DTOs;

public class TeacherConfigDTO {

    [Required]
    public required string TeacherId { get; set; }
    public string? FirstName {get; set;}
    public string? LastName {get; set;}
    public bool TimetableIsBiweekly {get; set;}
}