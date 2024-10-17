using System.ComponentModel.DataAnnotations;

namespace TeacherTimetabler.Api.DTOs;

public class ChangePasswordDTO
{
    [Required]
    public required string CurrentPassword {get; set;}

    [Required]
    public required string NewPassword {get; set;}

    [Required]
    [Compare("currentPassword", ErrorMessage = "The new password and confirmation password do not match")]
    public required string ConfirmPassword {get; set;}
}
