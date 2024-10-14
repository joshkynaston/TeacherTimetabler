using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TeacherTimetabler.Api.Models;

namespace TeacherTimetabler.Api.Controllers;

[ApiController]
[Route("api/account")]
public class AccountController(
    UserManager<UserEntity> userManager,
    SignInManager<UserEntity> signInManager
) : ControllerBase
{
    private readonly UserManager<UserEntity> _userManager = userManager;
    private readonly SignInManager<UserEntity> _signInManager = signInManager;

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterDTO registerDTO)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var user = new UserEntity
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

        if (result.Succeeded) {
            return Ok(new { message = "User logged in successfully" });
        }

        return Unauthorized( new { Error = "Unsuccessful login attempt" });
    }
}
