using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TeacherTimetabler.Api.Models;
using TeacherTimetabler.Api.Services;

namespace TeacherTimetabler.Api.Controllers;

[ApiController]
[Route("api/classes")]
public class ClassController(ClassService classService) : ApiControllerBase
{
    private readonly ClassService _classService = classService;

    [HttpGet("details")]
    [ProducesResponseType(typeof(ClassDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [Authorize]
    public async Task<IActionResult> GetClass([FromQuery] int? id, [FromQuery] string? name)
    {
        if (!TryGetUserId(out var userId))
        {
            return BadRequest("User not found");
        }

        if (id == null && string.IsNullOrEmpty(name))
        {
            return BadRequest("Either 'id' or 'name' must be provided.");
        }

        if (id.HasValue && !string.IsNullOrEmpty(name))
        {
            return BadRequest("Only one of 'id' or 'name' should be provided.");
        }

        ClassDTO? getClassDTO = null;

        if (id.HasValue)
        {
            getClassDTO = await _classService.GetClassByIdAsync(userId, id.Value);
            if (getClassDTO is null)
            {
                return NotFound($"Class with id {id} not found");
            }
        }

        if (!string.IsNullOrEmpty(name))
        {
            getClassDTO = await _classService.GetClassByNameAsync(userId, name);
            if (getClassDTO is null)
            {
                return NotFound($"Class with name {name} not found");
            }
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

        bool wasDeleted = await _classService.DeleteClassAsync(userId, id);
        if (!wasDeleted)
        {
            return NotFound(new { Error = $"Class with id {id} not found" });
        }
        else
        {
            return NoContent();
        }
    }
}
