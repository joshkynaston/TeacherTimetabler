using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TeacherTimetabler.Api.DTOs;
using TeacherTimetabler.Api.Models;

namespace TeacherTimetabler.Api.Controllers;

// TODO: This controller is doing far too much - offload some of this to the service layer
[ApiController]
[Route("api/account")]
public class AccountController(UserManager<Teacher> userManager, SignInManager<Teacher> signInManager) : ControllerBase
{
  private readonly UserManager<Teacher> _userManager = userManager;
  private readonly SignInManager<Teacher> _signInManager = signInManager;

  [HttpPost("register")]
  public async Task<IActionResult> Register([FromBody] RegisterDTO registerDTO)
  {
    if (!ModelState.IsValid)
    {
      return BadRequest(ModelState);
    }

    var user = new Teacher
    {
      UserName = registerDTO.Email,
      Email = registerDTO.Email,
      FirstName = registerDTO.FirstName,
      LastName = registerDTO.LastName,
    };

    var result = await _userManager.CreateAsync(user, registerDTO.Password);

    if (!result.Succeeded)
    {
      foreach (var error in result.Errors)
      {
        ModelState.AddModelError(string.Empty, error.Description);
      }
      return BadRequest(ModelState);
    }

    await _signInManager.SignInAsync(user, isPersistent: false);

    return Ok(new { message = "User created successfully" });
  }

  [HttpPost("login")]
  public async Task<IActionResult> Login([FromBody] LoginDTO loginDTO)
  {
    if (!ModelState.IsValid)
    {
      return BadRequest(ModelState);
    }

    var result = await _signInManager.PasswordSignInAsync(
      loginDTO.Email,
      loginDTO.Password,
      isPersistent: false,
      lockoutOnFailure: false
    );

    if (result.Succeeded)
    {
      return Ok(new { message = "User logged in successfully" });
    }

    return Unauthorized(new { Error = "Unsuccessful login attempt" });
  }

  [HttpPatch("config")]
  [Authorize]
  public async Task<IActionResult> Config([FromBody] TeacherConfigDTO configDTO)
  {
    Teacher? teacher = await _userManager.GetUserAsync(User);
    string? firstName = configDTO.FirstName;
    string? lastName = configDTO.LastName;

    if (teacher == null)
    {
      return Unauthorized(new { Error = "User not found" });
    }

    if (!string.IsNullOrEmpty(firstName))
    {
      teacher.FirstName = firstName;
    }

    if (!string.IsNullOrEmpty(lastName))
    {
      teacher.LastName = lastName;
    }

    teacher.TimetableIsBiweekly = configDTO.TimetableIsBiweekly;

    IdentityResult result = await _userManager.UpdateAsync(teacher);

    if (result.Succeeded)
    {
      return Ok(new { message = "Account configuration updated" });
    }
    else
    {
      return BadRequest(new { Error = "Failed to update account configuration" });
    }
  }
}
