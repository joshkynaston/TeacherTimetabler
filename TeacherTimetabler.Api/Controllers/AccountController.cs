using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TeacherTimetabler.Api.DTOs;
using TeacherTimetabler.Api.Models;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace TeacherTimetabler.Api.Controllers;

// TODO: This controller is doing far too much - offload some of this to the service layer
[ApiController]
[Route("api/account")]
public class AccountController(UserManager<Teacher> userManager, SignInManager<Teacher> signInManager) : ControllerBase
{
  private readonly UserManager<Teacher> _userManager = userManager;
  private readonly SignInManager<Teacher> _signInManager = signInManager;

  [HttpPost("register")]
  public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
  {
    if (!ModelState.IsValid)
      return BadRequest(ModelState);

    var user = new Teacher
    {
      UserName = registerDto.Email,
      Email = registerDto.Email,
      FirstName = registerDto.FirstName,
      LastName = registerDto.LastName,
    };

    IdentityResult? result = await _userManager.CreateAsync(user, registerDto.Password);

    if (!result.Succeeded)
    {
      foreach (IdentityError? error in result.Errors)
        ModelState.AddModelError(string.Empty, error.Description);
      return BadRequest(ModelState);
    }

    await _signInManager.SignInAsync(user, false);

    return Ok(new { message = "User created successfully" });
  }

  [HttpPost("login")]
  public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
  {
    if (!ModelState.IsValid)
      return BadRequest(ModelState);

    SignInResult? result = await _signInManager.PasswordSignInAsync(loginDto.Email, loginDto.Password, false, false);

    if (result.Succeeded)
      return Ok(new { message = "User logged in successfully" });

    return Unauthorized(new { Error = "Unsuccessful login attempt" });
  }

  [HttpPatch("config")]
  [Authorize]
  public async Task<IActionResult> Config([FromBody] TeacherConfigDto configDto)
  {
    Teacher? teacher = await _userManager.GetUserAsync(User);
    string? firstName = configDto.FirstName;
    string? lastName = configDto.LastName;

    if (teacher == null)
      return Unauthorized(new { Error = "User not found" });

    if (!string.IsNullOrEmpty(firstName))
      teacher.FirstName = firstName;

    if (!string.IsNullOrEmpty(lastName))
      teacher.LastName = lastName;

    teacher.TimetableIsBiweekly = configDto.TimetableIsBiweekly;

    IdentityResult? result = await _userManager.UpdateAsync(teacher);

    if (result.Succeeded)
      return Ok(new { message = "Account configuration updated" });
    else
      return BadRequest(new { Error = "Failed to update account configuration" });
  }
}
