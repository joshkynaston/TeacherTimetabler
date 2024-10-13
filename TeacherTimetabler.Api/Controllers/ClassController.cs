using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TeacherTimetabler.Api.Models.DTOs;
using TeacherTimetabler.Api.Services;

namespace TeacherTimetabler.Api.Controllers;

[ApiController]
[Route("api/classes")]
public class ClassController(ClassService classService) : ApiControllerBase
{
    private readonly ClassService _classService = classService;

    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(ClassDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [Authorize(Policy = "ResourceOwner")]
    public async Task<IActionResult> GetClassById(int id)
    {
        if (!TryGetUserId(out var userId))
        {
            return BadRequest(new { Error = "User not found" });
        }

        var getClassDTO = await _classService.GetClassByIdAsync(userId, id);
        if (getClassDTO is null)
        {
            return NotFound($"Class with id {id} not found");
        }
        return Ok(getClassDTO);
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<ClassDTO>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [Authorize]
    public async Task<IActionResult> GetClasses()
    {
        if (!TryGetUserId(out var userId))
        {
            return BadRequest(new { Error = "User not found" });
        }

        var result = await _classService.GetClassesAsync(userId);
        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(ClassDTO), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> PostClass(PostClassDTO postClassDTO)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userId is null)
        {
            return BadRequest(new { Error = "User not found" });
        }

        var (success, classDTO, location) = await _classService.CreateClassAsync(
            userId,
            postClassDTO
        );
        if (!success)
        {
            return BadRequest(new { Error = "Failed to create class." });
        }
        else
        {
            return Created(location, classDTO);
        }
    }

    [HttpDelete("{id:int}")]
    [Authorize(Policy = "ResourceOwner")]
    public async Task<IActionResult> DeleteClassById(int id)
    {
        if (!TryGetUserId(out var userId))
        {
            return BadRequest(new { Error = "User not found" });
        }

        (bool isDeleted, ClassDTO? classDTO) = await _classService.DeleteClassAsync(userId, id);
        if (!isDeleted)
        {
            return NotFound(new { Error = $"Class with id {id} not found" });
        } else {
            return Ok(classDTO);
        }
    }
}
