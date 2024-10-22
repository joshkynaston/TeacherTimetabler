using TeacherTimetabler.Api.Models;

namespace TeacherTimetabler.Api.DTOs;

public interface IEntityDTOFactory
{
  object? CreateDto(OwnedEntity entity);
}
