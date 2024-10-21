using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TeacherTimetabler.Api.DTOs;
using TeacherTimetabler.Api.Services;

namespace TeacherTimetabler.Api.Controllers;

[ApiController]
[Route("api/classes")]
public class ClassController(IClassService classService, ITeacherService userService) : ControllerBase
{
  private readonly IClassService _classService = classService;
  private readonly ITeacherService _userService = userService;

  // GET /api/classes
  [HttpGet]
  [ProducesResponseType(typeof(IEnumerable<GetClassDTO>), StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status401Unauthorized)]
  [ProducesResponseType(StatusCodes.Status403Forbidden)]
  [Authorize]
  public async Task<IActionResult> GetClasses()
  {
    var user = await _userService.GetCurrentUserAsync();

    if (user is null)
    {
      return BadRequest(new { Error = "User not found" });
    }

    IEnumerable<GetClassDTO> result = await _classService.GetClassesAsync(user.Id);
    return Ok(result);
  }

  // GET /api/classes/{id}
  [HttpGet("{classId:int}")]
  [ProducesResponseType(typeof(GetClassDTO), StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status400BadRequest)]
  [ProducesResponseType(StatusCodes.Status404NotFound)]
  [Authorize]
  public async Task<IActionResult> GetClass([FromRoute] int classId)
  {
    var user = await _userService.GetCurrentUserAsync();

    if (user is null)
    {
      return BadRequest(new { Error = "User not found" });
    }

    var classDTO = await _classService.GetClassByIdAsync(user.Id, classId);
    if (classDTO is null)
    {
      return NotFound($"Class with id {classId} not found");
    }

    return Ok(classDTO);
  }

  // POST /api/classes
  [HttpPost]
  [ProducesResponseType(typeof(GetClassDTO), StatusCodes.Status201Created)]
  [ProducesResponseType(StatusCodes.Status400BadRequest)]
  [Authorize]
  public async Task<IActionResult> PostClass(PostClassDTO postClassDTO)
  {
    if (!ModelState.IsValid)
    {
      return BadRequest(new { Error = "Invalid or incomplete class data" });
    }

    var user = await _userService.GetCurrentUserAsync();

    if (user is null)
    {
      return BadRequest(new { Error = "User not found" });
    }

    GetClassDTO? classDTO = await _classService.AddClassAsync(user.Id, postClassDTO);

    if (classDTO is null)
    {
      return BadRequest(new { Error = "Failed to create class." });
    }
    else
    {
      return Created($"/api/classes/{classDTO.Id}", classDTO);
    }
  }

  [HttpDelete("{classId:int}")]
  [Authorize]
  public async Task<IActionResult> DeleteClassById([FromRoute] int classId)
  {
    var user = await _userService.GetCurrentUserAsync();

    if (user is null)
    {
      return BadRequest(new { Error = "User not found" });
    }

    bool wasDeleted = await _classService.DeleteClassAsync(user.Id, classId);
    if (!wasDeleted)
    {
      return NotFound(new { Error = $"Class with id {classId} not found" });
    }
    else
    {
      return NoContent();
    }
  }
}
