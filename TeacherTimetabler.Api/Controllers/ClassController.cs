using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TeacherTimetabler.Api.DTOs;
using TeacherTimetabler.Api.Models;
using TeacherTimetabler.Api.Services;

namespace TeacherTimetabler.Api.Controllers;

[ApiController]
[Route("api/classes")]
public class ClassController(IClassService classService, ITeacherService userService) : ControllerBase
{
  // GET /api/classes
  [HttpGet]
  [ProducesResponseType(typeof(IEnumerable<GetClassDto>), StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status401Unauthorized)]
  [ProducesResponseType(StatusCodes.Status403Forbidden)]
  [Authorize]
  public async Task<IActionResult> GetClasses()
  {
    Teacher? user = await userService.GetCurrentUserAsync();

    if (user is null)
      return BadRequest(new { Error = "User not found" });

    IEnumerable<GetClassDto> result = await classService.GetClassesAsync(user.Id);
    return Ok(result);
  }

  // GET /api/classes/{classId}
  [HttpGet("{classId:int}")]
  [ProducesResponseType(typeof(GetClassDto), StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status400BadRequest)]
  [ProducesResponseType(StatusCodes.Status404NotFound)]
  [Authorize]
  public async Task<IActionResult> GetClass([FromRoute] int classId)
  {
    Teacher? user = await userService.GetCurrentUserAsync();

    if (user is null)
      return BadRequest(new { Error = "User not found" });

    GetClassDto? classDto = await classService.GetClassAsync(user.Id, classId);
    if (classDto is null)
      return NotFound($"Class with id {classId} not found");

    return Ok(classDto);
  }

  // POST /api/classes
  [HttpPost]
  [ProducesResponseType(typeof(GetClassDto), StatusCodes.Status201Created)]
  [ProducesResponseType(StatusCodes.Status400BadRequest)]
  [Authorize]
  public async Task<IActionResult> PostClass(PostClassDto postClassDto)
  {
    if (!ModelState.IsValid)
      return BadRequest(new { Error = "Invalid or incomplete class data" });

    Teacher? user = await userService.GetCurrentUserAsync();

    if (user is null)
      return BadRequest(new { Error = "User not found" });

    GetClassDto? classDto = await classService.AddClassAsync(user.Id, postClassDto);

    if (classDto is null)
      return BadRequest(new { Error = "Failed to create class." });

    return Created($"/api/classes/{classDto.ClassId}", classDto);
  }

  // DELETE /api/classes/{classId}
  [HttpDelete("{classId:int}")]
  [Authorize]
  public async Task<IActionResult> DeleteClassById([FromRoute] int classId)
  {
    Teacher? user = await userService.GetCurrentUserAsync();

    if (user is null)
      return BadRequest(new { Error = "User not found" });

    bool wasDeleted = await classService.DeleteClassAsync(user.Id, classId);
    if (!wasDeleted)
      return NotFound(new { Error = $"Class with id {classId} not found" });
    else
      return NoContent();
  }
}
