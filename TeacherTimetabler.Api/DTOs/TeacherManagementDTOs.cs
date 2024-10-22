using System.ComponentModel.DataAnnotations;

namespace TeacherTimetabler.Api.DTOs;

public record TeacherConfigDto(
  [Required] string TeacherId,
  string? FirstName,
  string? LastName,
  bool TimetableIsBiweekly
);

public record RegisterDto(
  [Required] [EmailAddress] string Email,
  [Required] [MinLength(8)] [DataType(DataType.Password)] string Password,
  [Required] string FirstName,
  [Required] string LastName
);

public record LoginDto([Required] [EmailAddress] string Email, [Required] string Password);

public record ChangePasswordDto
{
  [Required]
  public required string CurrentPassword { get; set; }

  [Required]
  public required string NewPassword { get; set; }

  [Required]
  [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match")]
  public required string ConfirmPassword { get; set; }
}
